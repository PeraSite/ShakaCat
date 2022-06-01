using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TypewriterUI : MonoBehaviour {
	private TMP_Text _text;

	[SerializeField]
	[Multiline]
	private string writer;

	[SerializeField]
	private float delayBeforeStart;

	[SerializeField]
	private float timeBtwChars = 0.1f;

	[SerializeField]
	private float timeBtwSentences = 0.1f;

	[SerializeField]
	private string leadingChar = "";

	[SerializeField]
	private bool leadingCharBeforeDelay;

	private void Awake() {
		_text = GetComponent<TMP_Text>();
		_text.text = "";
	}

	[Button]
	public void StartTypewrite() {
		StartCoroutine(nameof(DoTypewriter));
	}

	[Button]
	public Coroutine StartTypewrite(string text) {
		writer = text;
		return StartCoroutine(nameof(DoTypewriter));
	}

	public void StopTypewrite() {
		StopCoroutine(nameof(DoTypewriter));
	}

	private IEnumerator DoTypewriter() {
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (var c in writer) {
			if (_text.text.Length > 0) {
				_text.text = _text.text[..^leadingChar.Length];
			}
			_text.text += c;
			_text.text += leadingChar;
			yield return new WaitForSeconds(c == '\n' ? timeBtwSentences : timeBtwChars);
		}

		if (leadingChar != "") {
			_text.text = _text.text[..^leadingChar.Length];
		}
	}
}
