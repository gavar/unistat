using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniStat.Lib;

namespace UniStat.DataBinding {
	public class BoundUISlider : BoundFloat {
		private UISlider _slider;
		public bool SetTint=false;
		
		protected override void Setup ()
		{
			_slider = GetComponent<UISlider>();
		}
		
		protected override void UpdateView(float newValue)
		{
			_slider.sliderValue = newValue;
			
			if(SetTint && _slider){
				ColorHSV col = new ColorHSV();
				col._s = 1.0f;
				col._v = 1.0f;
				col._h = newValue * 140.0f;
				col._a = 1.0f;
				
				UISprite s = GetComponentInChildren<UISprite>();
				s.color = col.ToColor();
				
				UIFilledSprite f = GetComponentInChildren<UIFilledSprite>();
				f.color = col.ToColor();
			}	
			
			
		}
	}
}