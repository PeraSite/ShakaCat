using Sirenix.OdinInspector;
using UnityEngine;

public class IngredientData : SerializedScriptableObject {
	[Header("기본 정보")]
	public string Name;

	public IngredientCategory Category;

	[PreviewField(100f)]
	public Sprite BottleImage;

	public int UnlockPrice;
}

public enum IngredientCategory {
	기주,
	리큐르,
	시럽,
	주스,
	캔음료
}
