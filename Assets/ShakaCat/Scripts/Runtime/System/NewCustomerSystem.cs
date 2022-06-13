using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityAtoms;
using UnityEngine;

namespace ShakaCat {
	public class NewCustomerSystem : MonoBehaviour {
		[Header("변수")]
		public CustomerDataVariable CurrentCustomer;

		public IngredientDataValueList UnlockedIngredient;

		public ScriptableObjectCache SOCache;

		public CustomerDataEvent CustomerChangedEvent;

		[Header("사운드")]
		public SoundEffectSO NewCustomerSound;

		[Header("설정")]
		public float DelayAfterServe;

		private CustomerData _lastCustomer;

		private void Awake() {
			CustomerChangedEvent.Register(OnCustomerChanged);
		}

		private void OnDisable() {
			CustomerChangedEvent.Unregister(OnCustomerChanged);
			CurrentCustomer.Value = null;
		}

		private void Start() {
			MakeNewCustomer();
		}

		private void OnCustomerChanged(CustomerData data) {
			if (data.SafeIsUnityNull()) {
				MakeNewCustomer();
			}
		}

		public void MakeNewCustomer() {
			StartCoroutine(MakeNewCustomerCoroutine());
		}

		private IEnumerator MakeNewCustomerCoroutine() {
			yield return new WaitForSecondsRealtime(DelayAfterServe);
			var availableCustomers = GetAvailableCustomers().ToList();
			var newCustomer = availableCustomers.RandomOrNull();
			if (newCustomer.SafeIsUnityNull()) throw new Exception("Can't find new customer");
			CurrentCustomer.Value = newCustomer;
			_lastCustomer = newCustomer;
			NewCustomerSound.Play();
		}

		private IEnumerable<CustomerData> GetAvailableCustomers() {
			var availableCustomers = SOCache.Find<CustomerData>()
				.Where(customer => customer.NeedUnlockedIngredients.All(ing => UnlockedIngredient.Contains(ing)))
				.Where(customer => _lastCustomer.SafeIsUnityNull() || customer != _lastCustomer);
			return availableCustomers;
		}
	}
}
