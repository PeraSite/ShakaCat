using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CustomerData : SerializedScriptableObject {
	[Header("기본 정보")]
	public string Name;

	public CustomerType Type;

	[PreviewField(100f)]
	public Sprite Portrait;

	public DrinkData WantDrink;

	public List<IngredientData> NeedUnlockedIngredients = new();

	[Header("대화 관련")]
	[Multiline]
	public string GreetScript;

	public List<CustomerSelectionData> Selections = new();

	[Header("원하는 칵테일 지급 시")]
	[LabelText("보너스")]
	public float DrinkCorrectBonus;

	[LabelText("대사")]
	[Multiline]
	public string DrinkCorrectScript;

	[Header("다른 칵테일 지급 시")]
	[LabelText("보너스")]
	public float DrinkWrongBonus;

	[LabelText("대사")]
	[Multiline]
	public string DrinkWrongScript;

	[Header("힌트 스크립트를 봤는데 틀림")]
	[LabelText("보너스")]
	public float SawHintDrinkWrongBonus;

	[LabelText("대사")]
	[Multiline]
	public string SawHintDrinkWrongScript;


#if UNITY_EDITOR
	private void OnValidate() {
		var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
		Name = Path.GetFileNameWithoutExtension(assetPath);
	}
#endif
}

public struct CustomerSelectionData {
	public string Title;

	[Multiline]
	public string Reply;

	public float TipMultiplier;
}

public enum CustomerType {
	GHOST,
	ZOMBIE,
	DOLL,
}
