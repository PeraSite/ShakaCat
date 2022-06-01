using System;
using DG.Tweening;
using TMPro;
using UnityAtoms;
using UnityEngine;

namespace ShakaCat {
	public class DrinkMakeUI : MonoBehaviour {
		[Header("오브젝트")]
		public RectTransform DrinkMakePanel;

		public IngredientDataValueList CurrentIngredient;
		public TextMeshProUGUI currentIngredientText;

		[Header("설정")]
		public float HideX;

		public float AnimationTime;

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

		public void HideDrinkMakeUI() {
			DrinkMakePanel.DOAnchorPosX(HideX, AnimationTime);
		}
	}
}
