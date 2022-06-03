using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerData : SerializedScriptableObject {
	[Header("기본 정보")]
	public string Name;

	public CustomerType Type;

	public Sprite Portrait;

	[Multiline]
	public List<string> GreetScript;

	[Multiline]
	public List<string> ResultScript;
}

public enum CustomerType {
	GHOST,
	ZOMBIE,
	DRACULA,
}
