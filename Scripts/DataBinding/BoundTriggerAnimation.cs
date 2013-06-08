using UnityEngine;
using System.Collections;

namespace UniStat.DataBinding {
	public class BoundTriggerAnimation : MonoBehaviour, IChangeListener {
		private Animation _anim;
		public string DataID;
		public AnimationClip Clip;
		void Start(){
			_anim = GetComponent<Animation>();
			if(_anim == null){
				_anim = gameObject.AddComponent<Animation>();
			}
			_anim.AddClip(Clip,"Trigger");
			GameData.Subscribe(DataID,this);
		}
	
		#region IChangeListener implementation
		public void OnChange ()
		{
			if(_anim){
				_anim.CrossFade("Trigger");
			}
		}
		#endregion
	}
}