using UnityEngine;
using System.Collections;

namespace UniStat.DataBinding {
	public class BoundUITexture : MonoBehaviour, IChangeListener {
		public string DataID;
		private UITexture _tex;
		
		protected Texture2D currentValue {
			get {		
				return GameData.Get<Texture2D>(DataID);
			}
		}	
		
		void Start () {
			_tex = GetComponent<UITexture>();
			GameData.Subscribe(DataID,this);
			UpdateView(currentValue);
		}
		
		protected void UpdateView(Texture2D newValue){
			if(_tex != null)
				_tex.mainTexture = newValue;
		}
		
		#region IChangeListener implementation
		public void OnChange ()
		{
			UpdateView(currentValue);
		}
		#endregion
	}
}