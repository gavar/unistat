using UnityEngine;
using System.Collections;

namespace UniStat.DataBinding {
	public abstract class BoundFloat : MonoBehaviour, IChangeListener {
		public string DataID;
		
		protected float currentValue {
			get {
				object val;
				val = GameData.Get(DataID);
				if(val == null)
					return 0.0f;
				return (float)val;
			}
		}	
		
		void Start () {
			Setup ();
			GameData.Subscribe(DataID,this);
			UpdateView(currentValue);
		}
		
		protected virtual void Setup(){
			throw new System.NotImplementedException ();
		}
		
		protected virtual void UpdateView(float newValue){
			throw new System.NotImplementedException ();
		}
		
		#region IChangeListener implementation
		public void OnChange ()
		{
			UpdateView(currentValue);
		}
		#endregion
	}
}