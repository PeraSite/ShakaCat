using System;
using System.Collections;
using System.Collections.Generic;
using PeraCore.Runtime;
using Sirenix.Utilities;
using UnityAtoms;
using UnityEngine;

namespace ShakaCat {
	public class NewCustomerSystem : MonoBehaviour {
		[Header("변수")]
		public CustomerDataVariable CurrentCustomer;

		public List<CustomerData> AvailableCustomers;

		public CustomerDataEvent CustomerChangedEvent;

		[Header("설정")]
		public float DelayAfterServe;

		private void Awake() {
			CustomerChangedEvent.Register(OnCustomerChanged);
		}

		private void OnDisable() {
			CustomerChangedEvent.Unregister(OnCustomerChanged);
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
			var newCustomer = AvailableCustomers.RandomOrNull();
			if (newCustomer.SafeIsUnityNull()) throw new Exception("Can't find new customer");
			CurrentCustomer.Value = newCustomer;
		}
	}
}
