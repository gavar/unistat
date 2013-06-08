using UnityEngine;
using System.Collections;

namespace UniStat.DataBinding {
	public abstract class BoundString : MonoBehaviour, IChangeListener {
		public string DataID;
		public string Format;
		public string Lookup;
		public string DisableID;
		public string DisabledText;
		public bool Lowercase=false;
		
		protected string currentValue {
			get {
				if(DisableID != ""){
					bool b = GameData.Get<bool>(DisableID);
					if(!b)
						return DisabledText;
				}
				object val;
				string txt;
				if(Format != "" && Lookup == ""){
					val = GameData.Get(DataID,Format);
				}else{
					val = GameData.Get(DataID);
				}
				if(val == null)
					txt = "";
				else{
					if(Lookup != ""){
						val = GameData.Lookup(Lookup,val,Format);
					}
					if(val == null)
						return "";
					txt = val.ToString();
									
					if(Lowercase)
						txt = txt.ToLower();
				}
				return txt;
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
		
		protected virtual void UpdateView(string newValue){
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