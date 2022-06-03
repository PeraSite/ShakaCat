using System.Collections.Generic;
using PixelCrushers;
using UnityAtoms;
using UnityEngine;

namespace ShakaCat {
	public class UnlockedIngredientSaver : Saver {
		public IngredientDataValueList UnlockedIngredients;
		public List<IngredientData> DefaultIngredients;

		public override string RecordData() {
			return SaveSystem.Serialize(UnlockedIngredients.List);
		}

		public override void ApplyData(string data) {
			if (data == null) return;

			var ings = SaveSystem.Deserialize<List<IngredientData>>(data);
			UnlockedIngredients.Clear();
			foreach (var ing in ings) {
				UnlockedIngredients.Add(ing);
			}
		}

		public override void OnRestartGame() {
			UnlockedIngredients.Clear();
			foreach (var ing in DefaultIngredients) {
				UnlockedIngredients.Add(ing);
			}
		}
	}
}
