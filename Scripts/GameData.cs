using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UniStat {
	public static class GameData {
		public static Scene CurrentScene;
		private static Hashtable _data;
		private static Dictionary<string,List<IChangeListener>> _changeListeners;
		private static Dictionary<string,List<IChangeListener>> _lateChangeListeners;
		
		static GameData(){
			_data = new Hashtable();
			_changeListeners = new Dictionary<string, List<IChangeListener>>();
			_lateChangeListeners = new Dictionary<string, List<IChangeListener>>();
		}
		
		public static void Subscribe(string id, IChangeListener listener, bool late=false){
			if(id.Contains(".")){
				string[] parts = id.Split('.');
				id = parts[0];
			}
			if(!late){
				if(!_changeListeners.ContainsKey(id)){
					_changeListeners.Add(id, new List<IChangeListener>());
				}				
				_changeListeners[id].Add(listener);
			}else{
				if(!_lateChangeListeners.ContainsKey(id)){
					_lateChangeListeners.Add(id, new List<IChangeListener>());
				}				
				_lateChangeListeners[id].Add(listener);
			}
		}
		
		public static void Unsubscribe(string id, IChangeListener listener){
			_changeListeners[id].Remove(listener);		
		}
		
		public static string Get(string name,string format){
			object v = Get(name);
			if(v == null){
				return "";
			}
			if(v.GetType() == typeof(string)){
				string s = (string)v;
				return string.Format(format,s);
			}else if(v.GetType() == typeof(float)){
				float f = (float)v;
				return f.ToString(format);
			}else if(v.GetType() == typeof(int)){
				int f = (int)v;
				return f.ToString(format);
			}else if(v.GetType() == typeof(double)){
				double d = (double)v;
				return d.ToString(format);
			}else if(v.GetType() == typeof(System.Int64)){
				System.Int64 d = (System.Int64)v;
				return d.ToString(format);
			}
			return "";
		}
		
		public static T Get<T>(string name){
			object v = Get(name);
			if(v == null){
				return default(T);
			}
			return (T)v;
		}
		
		public static object Get(string name){
			object ob;
			if(name.Contains(".")){
				string[] parts = name.Split('.');
				if(!_data.ContainsKey(parts[0])){
					return null;
				}
				object val = _data[parts[0]];
				System.Type T = val.GetType();
				FieldInfo field = T.GetField(parts[1]);
				if(field != null){
					ob = field.GetValue(val);
				}else{
					PropertyInfo prop = T.GetProperty(parts[1]);
					if(prop != null){
						ob = prop.GetValue(val,null);
					}else{
						ob = null;
					}				
				}
				if(parts.Length > 2){
					IDictionary data = (IDictionary)ob;
					//Dictionary<System., TValue> data = (Dictionary<TKey, TValue>)ob;
					ob = data[parts[2]];				
				}
			}else{
				if(!_data.ContainsKey(name)){
					return null;
				}
				ob = _data[name];
			}
			return ob;
		}
		
		public static void LoadLevel(string name){
			_changeListeners.Clear();
			Application.LoadLevel(name);
		}
		
		public static object Lookup(string name, object id, string format=""){
			string[] parts = name.Split('.');
			object ob,v;
			if(!_data.ContainsKey(parts[0])){
				return null;
			}
			object val = _data[parts[0]];
			System.Type[] interfaces = val.GetType().GetInterfaces();
			bool isDict = false;
			foreach(System.Type i in interfaces){
				if(i == typeof(IDictionary))
					isDict = true;
			}
			if(isDict){
				IDictionary dict = (IDictionary)val;
				ob = dict[id];
			}else{
				int index = (int)id;
				IList list = (IList)val;
				if(list.Count == 0)
					return null;
				ob = list[index];
			}
			if(ob != null){
				System.Type T = ob.GetType();
				FieldInfo field = T.GetField(parts[1]);
				if(field != null){
					v = field.GetValue(ob);
				}else{
					PropertyInfo prop = T.GetProperty(parts[1]);
					if(prop != null){
						v = prop.GetValue(ob,null);
					}else{
						v = null;
					}				
				}
				if(format == ""){
					return v;
				}else{
					if(v == null){
						return "";
					}
					if(v.GetType() == typeof(string)){
						string s = (string)v;
						return string.Format(format,s);
					}else if(v.GetType() == typeof(float)){
						float f = (float)v;
						return f.ToString(format);
					}else if(v.GetType() == typeof(int)){
						int f = (int)v;
						return f.ToString(format);
					}
					return "";
				}
			}else{
				return null;
			}
		}
		
		public static void Put(string name, object ob){
			if(ob == null)
				_data.Remove(name);
			else
				_data[name] = ob;
			
			TriggerChangeEvent(name);
		}
		
		public static void TriggerChangeEvent(string name){
			if(_changeListeners.ContainsKey(name)){			
				foreach(IChangeListener l in _changeListeners[name]){
					l.OnChange();
				}
			}
			if(_lateChangeListeners.ContainsKey(name)){
				foreach(IChangeListener l in _lateChangeListeners[name]){
					l.OnChange();
				}
			}
		}
	}
}
