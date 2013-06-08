using UnityEngine;
using System.Collections;
using System.Reflection;

namespace UniStat.DataBinding {
	public class BoundTransform : MonoBehaviour, IChangeListener {
		public string DataID;
		
		protected Vector3 currentValue {
			get {
				object val;
				val = GameData.Get(DataID);
				if(val == null)
					return Vector3.zero;
				System.Type T = val.GetType();
				FieldInfo field = T.GetField("gameObject");
				object ob;
				if(field != null){
					ob = field.GetValue(val);
				}else{
					PropertyInfo prop = T.GetProperty("gameObject");
					if(prop != null){
						ob = prop.GetValue(val,null);
					}else{
						ob = null;
					}				
				}
				if(ob == null)
					return Vector3.zero;			
				
				GameObject go = (GameObject)ob;
				Vector3 pos = Camera.main.WorldToScreenPoint(go.transform.position);
				Vector3 p = new Vector3(Mathf.Round(pos.x),Mathf.Round(pos.y),0);			
				return p;
			}
		}	
		
		void Start () {
			GameData.Subscribe(DataID,this);
			UpdateView(currentValue);
		}
		
		void Update() {
			UpdateView(currentValue);
		}
		
		protected virtual void UpdateView(Vector3 newValue){
			gameObject.transform.localPosition = newValue;
			if(newValue == Vector3.zero)
				GetComponent<UIPanel>().enabled = false;
			else
				GetComponent<UIPanel>().enabled = true;
		}
		
		#region IChangeListener implementation
		public void OnChange ()
		{
			
		}
		#endregion
	}
}