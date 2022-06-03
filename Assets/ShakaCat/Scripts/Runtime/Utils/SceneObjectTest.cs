using UnityEngine;

namespace ShakaCat {
	public class SceneObjectTest : MonoBehaviour {
		private void Awake() {
			Debug.Log("Scene Object Awake");
		}

		private void OnEnable() {
			Debug.Log("Scene Object OnEnable");
		}

		private void Start() {
			Debug.Log("Scene Object Start");
		}
	}
}
