using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace ShakaCat {
	public class ResultUI : MonoBehaviour {
		[Header("음료 정보")]
		public DrinkDataVariable CurrentDrink;

		public FloatVariable CompletePercent;


		[Header("오브젝트")]
		public GameObject Completeness;

		public TextMeshProUGUI CompleteValue;
		public TextMeshProUGUI DrinkName;

		public Image ResultImage;

		public GameObject ServeButton;

		public Sprite InvalidRecipeImage;

		public GameObject InvalidRecipeTitle;

		public void UpdateResultUI() {
			var drinkData = CurrentDrink.Value;
			if (drinkData.SafeIsUnityNull()) { //레시피가 이상함
				Completeness.SetActive(false);
				DrinkName.gameObject.SetActive(false);
				ServeButton.SetActive(false);
				ResultImage.sprite = InvalidRecipeImage;
				InvalidRecipeTitle.SetActive(true);
				return;
			}

			ResetResultUI();
			var percent = CompletePercent.Value;

			CompleteValue.text = percent.ToString("F1") + "%";
			ResultImage.sprite = drinkData.Sprite;
			DrinkName.text = drinkData.Name;

		}


		public void ResetResultUI() {
			DrinkName.gameObject.SetActive(true);
			Completeness.SetActive(true);
			ServeButton.SetActive(true);
			InvalidRecipeTitle.SetActive(false);
		}
	}
}
