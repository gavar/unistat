using UnityEngine;
using System.Collections;

namespace UniStat.Lib.Noise {
	public abstract class NoiseGenerator {
		public abstract float Noise(float x, float y, float z);
	}
}