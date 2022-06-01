using System.Collections.Generic;
using PeraCore.Runtime;
using Sirenix.Utilities;
using UnityAtoms;
using UnityEngine;

namespace ShakaCat {
	public class NewCustomerSystem : MonoBehaviour {
		[Header("오브젝트")]
		public CustomerDataVariable CurrentCustomer;

		public List<CustomerData> AvailableCustomers;

		[Header("설정")]
		public float CheckCustomerTime = 5f;

		private float _timer;

		private void Update() {
			_timer += Time.deltaTime;
			if (_timer > CheckCustomerTime) {
				_timer = 0f;
				MakeNewCustomer();
			}
		}

		public void MakeNewCustomer() {
			if (!CurrentCustomer.Value.SafeIsUnityNull()) return;

			var newCustomer = AvailableCustomers.RandomOrNull();
			if (newCustomer.SafeIsUnityNull()) return;

			CurrentCustomer.Value = newCustomer;
		}
	}
}
