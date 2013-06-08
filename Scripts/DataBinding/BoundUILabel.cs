using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UniStat.DataBinding {
	public class BoundUILabel : BoundString {
		private UILabel _label;
		
		protected override void Setup ()
		{
			_label = GetComponent<UILabel>();
		}
		
		protected override void UpdateView(string newValue)
		{
			_label.text = newValue;
		}
	}
}