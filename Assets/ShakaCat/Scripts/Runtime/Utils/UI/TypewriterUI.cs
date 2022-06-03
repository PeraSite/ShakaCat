using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
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

	public bool IsWriting;

	private Coroutine _routine;
	private bool _shouldStop;

	private void Awake() {
		_text = GetComponent<TMP_Text>();
		_text.text = "";
	}

	[Button]
	public Coroutine StartTypewrite(string text) {
		writer = text;
		_shouldStop = false;
		_routine = StartCoroutine(nameof(DoTypewriter));
		return _routine;
	}

	public void StopTypewrite() {
		if (!IsWriting) return;
		_shouldStop = true;
		IsWriting = false;
	}

	public void SkipTypewrite() {
		StopTypewrite();
		_text.text = writer;
	}

	private IEnumerator DoTypewriter() {
		IsWriting = true;
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		if (_shouldStop) {
			yield break;
		}

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (var c in writer) {
			if (_shouldStop) {
				yield break;
			}

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
		IsWriting = false;
	}
}
