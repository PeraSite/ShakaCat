using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace PeraCore.Runtime {
	public abstract class MonoSingleton : SerializedMonoBehaviour {
		protected static MonoSingleton InstanceWeak { get; private protected set; }

		protected virtual bool KeepAlive => true;

		protected virtual void Awake() {
			if (!InstanceWeak.SafeIsUnityNull()) {
				Destroy(gameObject);
				return;
			}
			InstanceWeak = this;
			if (KeepAlive)
				DontDestroyOnLoad(gameObject);
		}

		protected virtual void OnDestroy() {
			InstanceWeak = null;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetInstance() {
			InstanceWeak = null;
		}
	}

	public abstract class MonoSingleton<T> : MonoSingleton where T : SerializedMonoBehaviour {
		public static T Instance => FindInstance();

		private static T FindInstance() {
			if (!InstanceWeak.SafeIsUnityNull()) return InstanceWeak as T;

			InstanceWeak = FindObjectOfType<T>() as MonoSingleton;
			return InstanceWeak as T;
		}
	}
}
