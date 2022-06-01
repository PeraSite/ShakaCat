using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerData : SerializedScriptableObject {
	[Header("기본 정보")]
	public string Name;

	public CustomerType Type;

	public Texture2D Portrait;

	[Multiline]
	public List<string> Script;
}

public enum CustomerType {
	GHOST,
	ZOMBIE,
	DRACULA,
}
