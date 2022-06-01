using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class DrinkData : SerializedScriptableObject {
	[Header("기본 정보")]
	public string Name;

	[MinMaxSlider(0, 10, true)]
	public Vector2Int ShakingTime;

	public Dictionary<IngredientData, int> Ingredients = new();

	[Header("잔 정보")]
	public GlassType GlassType;

	public GlassSize GlassSize;
}

public enum GlassType {
	하이볼,
	마가리타,
	칵테일,
	동_머그잔,
	풋티드_하이볼
}

public enum GlassSize {
	소량,
	중량,
	대량
}
