using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ShakaCat {
	public class ScriptableObjectCache : SerializedScriptableObject {
		public List<ScriptableObject> Cache = new();
	}
}
