using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A crude but effective way to do something that takes time using a coroutine, and update a UISlider with the progress.
/// </summary>
namespace UniStat {
	public abstract class LoadingScene : MonoBehaviour {
		protected List<ILoader> loaders;
		public UISlider Slider;
		//The number of operations to let through before returning control to Unity (ie the number of operations to attempt per frame)
		//High numbers can impact framerates, which is fine if this scene isnt doing anything else
		public int TicksPerFrame=1000; 
		private int _totalOperations=0;
		
		protected virtual void Setup(){
		}
		
		protected virtual void OnFinish(){
		}
		
		// Use this for initialization
		void Start () {
			loaders = new List<ILoader>();
			Setup ();
			
			foreach(ILoader loader in loaders){
				_totalOperations += loader.Count();
			}
			StartCoroutine("Load");
		}
		
		private IEnumerator Load(){
			int current = 0;
			foreach(ILoader loader in loaders){
				IEnumerator e = loader.Load();
				while(e.MoveNext()){
					current ++;
					Slider.sliderValue = (float)current / (float)_totalOperations;
					if(current % TicksPerFrame == 0)
						yield return null;
				}
			}
			OnFinish();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}