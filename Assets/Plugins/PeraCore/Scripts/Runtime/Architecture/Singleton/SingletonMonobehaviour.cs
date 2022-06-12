using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace PeraCore.Runtime {
	public abstract class MonoSingleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour {
		private static T _instance;

		public static T Instance => FindInstance();
		protected virtual bool KeepAlive => true;

		protected virtual void Awake() {
			if (!_instance.SafeIsUnityNull()) {
				Destroy(gameObject);
				return;
			}
			_instance = GetComponent<T>();
			if (KeepAlive)
				DontDestroyOnLoad(gameObject);
		}

		protected virtual void OnDestroy() {
			_instance = null;
		}

#if UNITY_EDITOR
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetInstance() {
			_instance = null;
		}
#endif

		private static T FindInstance() {
			if (!_instance.SafeIsUnityNull()) return _instance;

			_instance = FindObjectOfType(typeof(T)) as T;
			return _instance;
		}
	}
}
