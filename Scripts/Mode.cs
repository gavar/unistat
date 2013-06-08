using UnityEngine;
using System.Collections;

namespace UniStat {
	public abstract class Mode {
		public Scene ParentScene;
		public abstract bool Update();
		public float TimeElapsed;
		public virtual bool OnGUI(){
			return false;
		}
		public virtual void Cleanup(){
		}
		protected bool GetMouseButton(int btn){
			return Input.GetMouseButton(btn) && GUIUtility.hotControl == 0;
		}
		public virtual void Activated(){
			
		}
	}
}
