using UnityEngine;
using System.Collections;

public interface ILoader {
	int Count();
	IEnumerator Load();
}
