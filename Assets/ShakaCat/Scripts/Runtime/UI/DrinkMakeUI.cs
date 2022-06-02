using TMPro;
using UnityAtoms;
using UnityEngine;

namespace ShakaCat {
	public class DrinkMakeUI : MonoBehaviour {
		[Header("오브젝트")]
		public IngredientDataValueList CurrentIngredient;

		public TextMeshProUGUI currentIngredientText;

		private void Awake() {
			CurrentIngredient.Added.Register(OnAdded);
			CurrentIngredient.Cleared.Register(OnCleared);
		}

		private void OnDisable() {
			CurrentIngredient.Added.Unregister(OnAdded);
			CurrentIngredient.Cleared.Unregister(OnCleared);
		}

		private void OnAdded(IngredientData obj) {
			currentIngredientText.text += obj.Name + "\n";
		}

		private void OnCleared() {
			currentIngredientText.text = "";
		}
	}
}
