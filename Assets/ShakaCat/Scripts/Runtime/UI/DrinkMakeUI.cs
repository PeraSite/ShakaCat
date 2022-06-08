using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace ShakaCat {
	public class DrinkMakeUI : MonoBehaviour {
		[Header("오브젝트")]
		public TextMeshProUGUI currentIngredientText;

		[Header("변수")]
		public IngredientDataValueList CurrentIngredient;

		public IntVariable Money;

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

		public void ClearCurrentIngredient() {
			CurrentIngredient.ForEach(ing => Money.Add(ing.Price));
			CurrentIngredient.Clear();
		}
	}
}
