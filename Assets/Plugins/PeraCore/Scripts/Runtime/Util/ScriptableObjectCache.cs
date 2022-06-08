using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ShakaCat {
	public class ScriptableObjectCache : SerializedScriptableObject {
		public List<ScriptableObject> Cache = new();

		public IEnumerable<T> Find<T>() {
			return Cache.OfType<T>();
		}
	}
}
