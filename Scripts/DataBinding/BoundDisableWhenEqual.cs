using UnityEngine;
using System.Collections;

namespace UniStat.DataBinding {
	public class BoundDisableWhenEqual : MonoBehaviour, IChangeListener {
		public string DataID;
		public string DataID2;
		void Start(){
			GameData.Subscribe(DataID,this);
			GameData.Subscribe(DataID2,this);
		}
		void Update()
		{
			if(GameData.Get(DataID) == GameData.Get(DataID2))
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