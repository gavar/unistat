using UnityEngine;
using System.Collections;

namespace UniStat.Lib {
	public static class Probability {
		public static string Chance(System.Random rand, Hashtable chance,bool canBeNull=true){
			int upper = 100;
			if(!canBeNull){
				upper = 0;
				foreach(DictionaryEntry d in chance){
					upper += (int)d.Value;
				}
			}
			int r = rand.Next(upper);
			int current = 0;
			foreach(DictionaryEntry d in chance){
				if(r >= current && r < (current + (int)d.Value)){
					return (string)d.Key;
				}
				current += (int)d.Value;
			}
			return "";
		}
	}
}
