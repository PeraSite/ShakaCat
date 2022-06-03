using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class DrinkData : SerializedScriptableObject {
	[Header("기본 정보")]
	public string Name;

	[MinMaxSlider(0, 10, true)]
	public Vector2Int ShakeCount;

	public Dictionary<IngredientData, int> Ingredients = new();

	[PreviewField(100f)]
	public Sprite Sprite;

	public int Price;
}
