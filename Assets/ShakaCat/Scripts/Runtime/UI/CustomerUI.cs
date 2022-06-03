using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace ShakaCat {
	public class CustomerUI : MonoBehaviour, IAtomListener<CustomerData> {
		[Header("오브젝트")]
		public GameObject DialoguePanel;

		public TypewriterUI DialogueText;
		public GameObject StartMakingButton;

		public Image Portrait;

		[Header("변수")]
		public CustomerDataVariable CurrentCustomer;

		public CustomerDataEvent NewCustomerEvent;

		public DrinkDataVariable CurrentDrink;
		public FloatVariable CompletePercent;

		[Header("설정")]
		public bool AlwaysShowStartMakingButton;

		public float WaitAfterCustomerDisappear;

		private void Awake() {
			NewCustomerEvent.RegisterListener(this);
		}

		private void Start() {
			ResetUI();
		}

		private void OnDisable() {
			NewCustomerEvent.UnregisterListener(this);
		}

		public async void OnEventRaised(CustomerData item) {
			if (item.SafeIsUnityNull()) {
				ResetUI();
				return;
			}

			DialoguePanel.SetActive(true);
			Portrait.gameObject.SetActive(true);

			Portrait.sprite = item.Portrait;
			var script = item.GreetScript.RandomOrNull();
			if (script == null) throw new Exception("Can't find script for " + item.Name);
			await ShowDialogue(script).ToUniTask(this);
			StartMakingButton.SetActive(true);
		}

		private IEnumerator ShowDialogue(string script) {
			yield return DialogueText.StartTypewrite(script);
		}

		[Button]
		public void SkipDialogue() {
			DialogueText.SkipTypewrite();
		}

		private void ResetUI() {
			DialoguePanel.SetActive(false);
			Portrait.gameObject.SetActive(false);
			DialogueText.StopTypewrite();
			StartMakingButton.SetActive(AlwaysShowStartMakingButton);
		}

		public void OnServe() {
			var script = CurrentCustomer.Value.ResultScript.RandomOrNull();
			if (script == null) throw new Exception("Can't find greeting script!");
			StartMakingButton.SetActive(AlwaysShowStartMakingButton);
			StartCoroutine(ShowResultCoroutine(script));
		}

		private IEnumerator ShowResultCoroutine(string script) {
			yield return ShowDialogue(script);
			yield return new WaitForSecondsRealtime(WaitAfterCustomerDisappear);
			CurrentCustomer.Value = null;
			CurrentDrink.Value = null;
			CompletePercent.Value = -1f;
		}
	}
}
