using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ShakaCat {
	public class RecipePopup : MonoBehaviour {
		[Header("오브젝트")]
		public TextMeshProUGUI Title;

		public TextMeshProUGUI Description;
		public TextMeshProUGUI Ingredient;

		[Header("변수")]
		public ScriptableObjectCache SOCache;

		private List<DrinkData> _drinks;
		private int _currentIndex = 0;

		private void Start() {
			_drinks = SOCache.Find<DrinkData>().ToList();
			SetRecipeUI(_currentIndex);
		}

		[Button]
		public void SetRecipeUI(int index) {
			var drinkData = _drinks[index];
			_currentIndex = index;
			Title.text = drinkData.Name;
			Description.text = drinkData.Description;
			Ingredient.text = "";
			foreach (var (ing, amount) in drinkData.Ingredients) {
				Ingredient.text += $"{ing.Name} {amount}샷\n";
			}
			Ingredient.text += $"\n{drinkData.ShakeCount}번 흔들기";
		}

		[Button]
		public void ShowPreviousDrink() {
			SetRecipeUI(mod((_currentIndex - 1), _drinks.Count));
		}

		[Button]
		public void ShowNextDrink() {
			SetRecipeUI(mod((_currentIndex + 1), _drinks.Count));
		}

		private static int mod(int x, int m) {
			return (x % m + m) % m;
		}
	}
}
