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

		private IEnumerator ShowDialogue(string script) {
			yield return DialogueText.StartTypewrite(script);
			StartMakingButton.SetActive(true);
		}

		private void ResetUI() {
			DialoguePanel.SetActive(false);
			Portrait.gameObject.SetActive(false);
			StartMakingButton.SetActive(false);
			DialogueText.StopTypewrite();
		}
	}
}
