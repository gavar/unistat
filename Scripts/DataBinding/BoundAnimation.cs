using UnityEngine;
using System.Collections;

namespace UniStat.DataBinding {
	public class BoundAnimation : MonoBehaviour, IChangeListener {
		private Animation _anim;
		public string DataID;
		public AnimationClip OnClip;
		public AnimationClip OffClip;
		private bool isOn;
		void Start(){
			_anim = GetComponent<Animation>();
			if(_anim == null){
				_anim = gameObject.AddComponent<Animation>();
			}
			_anim.AddClip(OnClip,"On");
			_anim.AddClip(OffClip,"Off");
			GameData.Subscribe(DataID,this);
		}
	
		#region IChangeListener implementation
		public void OnChange ()
		{
			if(_anim){
				if(GameData.Get (DataID) == null && isOn){
					isOn = false;				
					_anim.CrossFade("Off");
				}else if(GameData.Get (DataID) != null && !isOn){
					isOn = true;
					_anim.CrossFade("On");
				}
			}
		}
		#endregion
	}
}
