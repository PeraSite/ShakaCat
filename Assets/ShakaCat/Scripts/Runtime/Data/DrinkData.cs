using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DrinkData : SerializedScriptableObject {
	[Header("기본 정보")]
	public string Name;

	[Multiline]
	public string Description;

	public int ShakeCount;

	public Dictionary<IngredientData, int> Ingredients = new();

	[PreviewField(100f)]
	public Sprite Sprite;

	public int Price;

#if UNITY_EDITOR
	private void OnValidate() {
		var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
		Name = Path.GetFileNameWithoutExtension(assetPath);
	}
#endif
}
