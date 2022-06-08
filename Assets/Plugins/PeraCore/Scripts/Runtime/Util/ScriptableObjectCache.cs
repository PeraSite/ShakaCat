﻿using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ShakaCat {
	public class ScriptableObjectCache : SerializedScriptableObject {
		public List<ScriptableObject> Cache = new();
		public string[] ScanPath = { };

		public IEnumerable<T> Find<T>() {
			return Cache.OfType<T>();
		}

#if UNITY_EDITOR

		private static ScriptableObjectCache _instance;

		private static void HandlePlayModeStateChange(PlayModeStateChange state) {
			if (state == PlayModeStateChange.ExitingEditMode) {
				_instance.UpdateCache();
			}
		}

		private void OnEnable() {
			_instance = this;
			if (EditorSettings.enterPlayModeOptionsEnabled) {
				EditorApplication.playModeStateChanged -= HandlePlayModeStateChange;
				EditorApplication.playModeStateChanged += HandlePlayModeStateChange;
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetInstance() {
			_instance = null;
		}

		[Button]
		private void UpdateCache() {
			var guids = AssetDatabase.FindAssets("t:scriptableobject", ScanPath);
			Cache.Clear();
			foreach (var guid in guids) {
				var assetPath = AssetDatabase.GUIDToAssetPath(guid);
				var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
				Cache.Add(asset);
			}
		}
#endif
	}
}
