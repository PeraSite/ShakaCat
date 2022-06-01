using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ShakaCat {
	public class ScriptableObjectCacheManager : SerializedMonoBehaviour {
		public string[] ScanPath = { };
		public ScriptableObjectCache Cache;

#if UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void SubsystemRegistration() {
			UpdateCache();
		}

		private static void UpdateCache() {
			var cache = FindObjectOfType<ScriptableObjectCacheManager>();
			var guids = AssetDatabase.FindAssets("t:scriptableobject",
				cache.ScanPath);
			cache.Cache.Cache.Clear();
			foreach (var guid in guids) {
				var assetPath = AssetDatabase.GUIDToAssetPath(guid);
				var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
				cache.Cache.Cache.Add(asset);
			}
		}
#endif
	}
}
