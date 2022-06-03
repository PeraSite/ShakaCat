using System.Collections.Generic;
using System.Linq;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace ShakaCat {
	public class ResultCalculateSystem : MonoBehaviour {
		public DrinkDataVariable CurrentDrink;
		public FloatVariable CompletePercent;
		public IngredientDataValueList CurrentIngredient;
		public IntVariable ShakeCounter;
		public ScriptableObjectCache Cache;
		public IntVariable Money;

		public VoidEvent ResultCalculatedEvent;

		private IEnumerable<DrinkData> Drinks => Cache.Find<DrinkData>();

		public void CalculateResult() {
			var drinkData = FindMatchedDrinkData();
			var completePercent = CalculateCompletePercent();

			CurrentDrink.Value = drinkData;
			CompletePercent.Value = completePercent;

			ResultCalculatedEvent.Raise();
		}

		public void ResetResult() {
			CurrentIngredient.Clear();
			ShakeCounter.Reset(true);
		}

		public void Serve() {
//TODO: 가격 계산 공식
			Money.Add(CurrentDrink.Value.Price);

			ResetResult();
		}

		[Button]
		private float CalculateCompletePercent() {
			var drinkData = FindMatchedDrinkData();
			if (drinkData.SafeIsUnityNull()) return -1f;

			var result = 100f;

			var targetIngredientCount = drinkData.Ingredients;
			var currentIngredientCount = GetIngredientCount();

			var ingredientCountDiff = 0;
			foreach (var (targetIngredient, targetAmount) in targetIngredientCount) {
				var currentAmount = currentIngredientCount[targetIngredient];
				Debug.Log($"{targetIngredient.Name}: 목표={targetAmount}, 현재={currentAmount}");
				ingredientCountDiff += Mathf.Abs(targetAmount - currentAmount);
			}
			result -= ingredientCountDiff * 10;


			var targetShakeCount = drinkData.ShakeCount;
			var currentShakeCount = ShakeCounter.Value;
			if (targetShakeCount.x < currentShakeCount || targetShakeCount.y > currentShakeCount) {
				var xDiff = Mathf.Abs(targetShakeCount.x - currentShakeCount);
				var yDiff = Mathf.Abs(targetShakeCount.y - currentShakeCount);

				var diff = xDiff > yDiff ? yDiff : xDiff;
				Debug.Log($"{drinkData.Name}: 목표 셰이킹={targetShakeCount}, 현재={currentShakeCount}, 차이={diff}");
				result -= diff * 5;
			}

			//TODO: 완성도 퍼센트 계산
			result = Mathf.Clamp(result, 0, 100);
			return result;
		}

		[Button]
		private DrinkData FindMatchedDrinkData() {
			var ingredientCount = GetIngredientCount();
			var currentIngredientTypes = ingredientCount.Keys.ToHashSet();

			return Drinks.FirstOrDefault(drink => {
				var targetIngredients = drink.Ingredients.Keys.ToHashSet();
				return ScrambledEquals(targetIngredients, currentIngredientTypes,
					new ReferenceEqualityComparer<IngredientData>());
			});
		}

		[Button]
		private Dictionary<IngredientData, int> GetIngredientCount() {
			var dict = new Dictionary<IngredientData, int>();
			foreach (var ing in CurrentIngredient) {
				dict[ing] = dict.GetOrPut(ing, 0) + 1;
			}
			return dict;
		}

		private static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2,
			IEqualityComparer<T> comparer) {
			var cnt = new Dictionary<T, int>(comparer);
			foreach (var s in list1) {
				if (cnt.ContainsKey(s)) {
					cnt[s]++;
				} else {
					cnt.Add(s, 1);
				}
			}
			foreach (var s in list2) {
				if (cnt.ContainsKey(s)) {
					cnt[s]--;
				} else {
					return false;
				}
			}
			return cnt.Values.All(c => c == 0);
		}
	}
}
