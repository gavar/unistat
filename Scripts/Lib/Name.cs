using UnityEngine;
using System.Collections;


namespace UniStat.Lib {
	/// <summary>
	/// Provides some basic random letter methods and an elite-style name generation
	/// </summary>
	public static class Name {
		public static string RandomLetter(System.Random r)
	    {
			string ret = "";
			int num = r.Next(0, 26); // Zero to 25
			char let = (char)('A' + num);
			return ret + let;
	    }
		public static string RandomLetter(System.Random r, int n)
	    {
			string ret = "";
			for(int i=0;i<n;i++){
				int num = r.Next(0, 26); // Zero to 25
				char let = (char)('A' + num);
				ret += let;
			}
			return ret;
	    }
		public static string GenerateName(System.Random r)
		{
			//adapted from the original elite method, with a changed digraph
			string digraphs = "fafemalunabararerixevivoine.pazizozutatetitotugoatse..";
			int max = digraphs.Length-3;
			int length = 5 + r.Next(0,5);
			string name = "";
			while(name.Length < length){
				int rand = r.Next(0,max);
				string sub = digraphs.Substring(rand,3);
				
				if(!name.Contains(sub)){
					name += sub;
				}
			}
			name = name.Replace(".","");
			return char.ToUpper(name[0]) + name.Substring(1);
		}
		public static string Int2Roman(int number){
			string[] numerals = new string[12]{"I","II","III","IV","V","VI","VII","VIII","IX","X","XI","XII"};
			return numerals[number-1];
		}
	}
}