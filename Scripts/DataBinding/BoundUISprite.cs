using UnityEngine;
using System.Collections;

namespace UniStat.DataBinding {
	public class BoundUISprite : BoundString {
		private UISprite _sprite;
		
		// Use this for initialization
		protected override void Setup () {
			_sprite = GetComponent<UISprite>();
		}
		
		protected override void UpdateView(string newValue)
		{
			if(_sprite == null)
				return;
			if(newValue == ""){
				_sprite.enabled = false;
			}else{
				_sprite.spriteName = newValue;
				_sprite.enabled = true;
				//transform.localScale = new Vector3(_sprite.sprite.outer.width,_sprite.sprite.outer.height,1);
			}
		}
	}
}