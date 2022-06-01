using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `CustomerData`. Inherits from `AtomVariable&lt;CustomerData, CustomerDataPair, CustomerDataEvent, CustomerDataPairEvent, CustomerDataCustomerDataFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/CustomerData", fileName = "CustomerDataVariable")]
    public sealed class CustomerDataVariable : AtomVariable<CustomerData, CustomerDataPair, CustomerDataEvent, CustomerDataPairEvent, CustomerDataCustomerDataFunction>
    {
        protected override bool ValueEquals(CustomerData other) {
            return _value == other;
        }
    }
}
