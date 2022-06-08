using System;
using System.Collections;
using System.Collections.Generic;
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
		public List<GameObject> SelectionButtons;

		public Image Portrait;

		[Header("변수")]
		public CustomerDataVariable CurrentCustomer;

		public CustomerDataEvent NewCustomerEvent;
		public IntVariable Money;

		public DrinkDataVariable CurrentDrink;
		public FloatVariable CompletePercent;

		public BoolVariable HasSeenHint;

		[Header("설정")]
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

		public void OnEventRaised(CustomerData item) {
			if (item.SafeIsUnityNull()) {
				ResetUI();
				return;
			}

			DialoguePanel.SetActive(true);
			Portrait.gameObject.SetActive(true);
			Portrait.sprite = item.Portrait;

			var greetingScript = item.GreetScript;
			if (greetingScript == null) throw new Exception("Can't find script for " + item.Name);
			StartCoroutine(ShowDialogue(greetingScript,
				() => { SelectionButtons.ForEach(button => button.SetActive(true)); }));
		}

		public void OnSelectionSelected(int index) {
			SelectionButtons.ForEach(button => button.SetActive(false));
			var selectionData = CurrentCustomer.Value.Selections[index];
			DialogueText.SkipTypewrite();
			if (selectionData.Reply == "") {
				StartMakingButton.SetActive(true);
			} else {
				StartCoroutine(ShowDialogue(selectionData.Reply, () => { StartMakingButton.SetActive(true); }));
			}
		}

		private IEnumerator ShowDialogue(string script, Action onDone = null) {
			yield return DialogueText.StartTypewrite(script);
			onDone?.Invoke();
		}

		[Button]
		public void SkipDialogue() {
			DialogueText.SkipTypewrite();
		}

		private void ResetUI() {
			DialoguePanel.SetActive(false);
			Portrait.gameObject.SetActive(false);
			DialogueText.StopTypewrite();
			SelectionButtons.ForEach(button => button.SetActive(false));
			StartMakingButton.SetActive(false);
		}

#region Result

		public void OnServe() {
			var wantDrink = CurrentCustomer.Value.WantDrink;
			var currentDrink = CurrentDrink.Value;
			string script;
			float tip;

			if (wantDrink == currentDrink) {
				script = CurrentCustomer.Value.DrinkCorrectScript;
				tip = CurrentCustomer.Value.DrinkCorrectBonus;
			} else {
				if (HasSeenHint.Value) {
					script = CurrentCustomer.Value.SawHintDrinkWrongScript;
					tip = CurrentCustomer.Value.SawHintDrinkWrongBonus;
				} else {
					script = CurrentCustomer.Value.DrinkWrongScript;
					tip = CurrentCustomer.Value.DrinkWrongBonus;
				}
			}

			StartMakingButton.SetActive(false);
			SelectionButtons.ForEach(button => button.SetActive(false));
			Money.Value = Mathf.Clamp(CurrentDrink.Value.Price + (int) tip, 0, int.MaxValue);
			StartCoroutine(ShowResultCoroutine(script));
		}

		private IEnumerator ShowResultCoroutine(string script) {
			yield return ShowDialogue(script);
			yield return new WaitForSecondsRealtime(WaitAfterCustomerDisappear);
			CurrentCustomer.Value = null;
			CurrentDrink.Value = null;
			CompletePercent.Value = -1f;
			HasSeenHint.Value = false;
		}

#endregion
	}
}
