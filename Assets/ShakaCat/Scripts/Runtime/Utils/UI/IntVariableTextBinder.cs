using Sirenix.Utilities;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace ShakaCat {
	public class IntVariableTextBinder : MonoBehaviour {
		public TextMeshProUGUI Text;
		public IntVariable Variable;

		public string Prefix;
		public string Suffix;

		private void OnValidate() {
			if (Text.SafeIsUnityNull()) Text = GetComponent<TextMeshProUGUI>();
		}

		private void Awake() {
			Variable.Changed.Register(OnChanged);
		}

		private void OnDisable() {
			Variable.Changed.Unregister(OnChanged);
		}

		private void OnChanged(int value) {
			Text.text = Prefix + value + Suffix;
		}
	}
}
