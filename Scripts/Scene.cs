using UnityEngine;
using System.Collections;

namespace UniStat {
	public abstract class Scene : MonoBehaviour {
		private Mode _currentMode;
		private bool _doneLateSetup;
		protected float _timeElapsed;
		public Mode CurrentMode{
			set {
				if(_currentMode != null)
					_currentMode.Cleanup();
				_currentMode = value;
				if(_currentMode != null){
					_currentMode.ParentScene = this;
					_currentMode.TimeElapsed = 0;
					_currentMode.Activated();				
				}
			}
			get {
				return _currentMode;
			}
		}
		public ArrayList Lines;
		public Mode DefaultMode;
		
		void Start(){
			_timeElapsed = 0;
			Lines = new ArrayList();
			Setup ();
			CurrentMode = DefaultMode;
			GameData.CurrentScene = this;
		}
		
		void Update() {
			_timeElapsed += Time.deltaTime;
			if(!_doneLateSetup){
				LateSetup();
				_doneLateSetup = true;
			}
			UpdateScene();
	
			if(CurrentMode != null){
				if(CurrentMode.Update()){
					CurrentMode = DefaultMode;
				}
				_currentMode.TimeElapsed += Time.deltaTime;
			}
		}
		
		protected virtual void UpdateScene() {
			
		}
		
		protected virtual void Setup(){
			
		}
		protected virtual void LateSetup(){
			
		}
		
	}
}
