using UnityEngine;
using System.Collections;

/// <summary>
/// For those times you just need to attach something to a GameObject that can be retrieved when a collision happens or something.
/// </summary>
namespace UniStat {
	public class AttachedObject : MonoBehaviour {
		public object Attached;	
	}
}