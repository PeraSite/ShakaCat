using System;
using System.Collections;
using PeraCore.Runtime;
using Sirenix.Utilities;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace ShakaCat {
	public class CustomerUI : MonoBehaviour, IAtomListener<CustomerData> {
		[Header("오브젝트")]
		public CustomerDataEvent NewCustomerEvent;

		public GameObject DialoguePanel;
		public TypewriterUI DialogueText;
		public GameObject StartMakingButton;

		public Image Portrait;

		[Header("설정")]
		public bool ShouldDeactivateButtonWhenDialogue;

		private void Awake() {
			NewCustomerEvent.RegisterListener(this);
		}

		private void Start() {
			ResetUI();
		}

		private void OnDisable() {
			NewCustomerEvent.UnregisterListener(this);
		}

		public void OnEventRaised(CustomerData item) {
			if (item.SafeIsUnityNull()) {
				ResetUI();
				return;
			}

			DialoguePanel.SetActive(true);
			Portrait.gameObject.SetActive(true);

			Portrait.sprite = item.Portrait;
			var script = item.Script.RandomOrNull();
			if (script == null) throw new Exception("Can't find script for " + item.Name);
			StartCoroutine(ShowDialogue(script));
		}

		private bool _isTypewriteActive;
		private Coroutine _typewriteTask;

		private IEnumerator ShowDialogue(string script) {
			_isTypewriteActive = true;
			yield return _typewriteTask = DialogueText.StartTypewrite(script);
			_isTypewriteActive = false;

			if (ShouldDeactivateButtonWhenDialogue)
				StartMakingButton.SetActive(true);
		}

		public void SkipDialogue() {
			if (!_isTypewriteActive) return;
			StopCoroutine(_typewriteTask);
			DialogueText.SkipTypewrite();
			_isTypewriteActive = false;

			if (ShouldDeactivateButtonWhenDialogue)
				StartMakingButton.SetActive(true);
		}

		private void ResetUI() {
			DialoguePanel.SetActive(false);
			Portrait.gameObject.SetActive(false);
			DialogueText.StopTypewrite();

			if (ShouldDeactivateButtonWhenDialogue)
				StartMakingButton.SetActive(false);
		}
	}
}
