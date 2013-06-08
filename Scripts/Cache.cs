using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A much simpler cut-down version of GameData for short term storage that isn't meant to be saved to disk.
/// </summary>
namespace UniStat {
	public static class Cache {
		private static Dictionary<string,object> _items;
		static Cache(){
			_items = new Dictionary<string, object>();
		}
		public static T Get<T>(string id){
			if(_items.ContainsKey(id)){
				return (T)_items[id];
			}else{
				return default(T);
			}
		}
		public static void Put(string id, object item){
			_items[id] = item;
		}
		public static bool Has(string id){
			return _items.ContainsKey(id);
		}
	}
}