using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerData : SerializedScriptableObject {
	[Header("기본 정보")]

	public string Name;

	public CustomerType Type;

	public Texture2D Portrait;
}

public enum CustomerType {
	GHOST,
	ZOMBIE,
	DRACULA,
}
