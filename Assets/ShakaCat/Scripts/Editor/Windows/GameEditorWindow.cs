using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class GameEditorWindow : OdinMenuEditorWindow {
	[MenuItem("Tools/데이터베이스")]
	private static void OpenWindow() {
		var window = GetWindow<GameEditorWindow>();
		window.name = "데이터베이스";
		window.Show();
	}

	private const string RESOURCE_PATH = "Assets/ShakaCat/Data/";

	protected override OdinMenuTree BuildMenuTree() {
		var tree = new OdinMenuTree {
			{"손님", null, EditorIcons.SingleUser},
			{"음료", null, EditorIcons.FileCabinet},
			{"재료", null, EditorIcons.File}
		};

		void AddAssets(string menuName, string pathName, Type type) {
			tree.AddAllAssetsAtPath(menuName, RESOURCE_PATH + pathName, type, true, true);
		}

		AddAssets("손님", "Customers", typeof(CustomerData));
		AddAssets("음료", "Drinks", typeof(DrinkData));
		AddAssets("재료", "Ingredients", typeof(IngredientData));

		return tree;
	}
}
