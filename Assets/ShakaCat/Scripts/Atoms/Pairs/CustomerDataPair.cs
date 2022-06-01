using System;
using UnityEngine;
using ShakaCat;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;CustomerData&gt;`. Inherits from `IPair&lt;CustomerData&gt;`.
    /// </summary>
    [Serializable]
    public struct CustomerDataPair : IPair<CustomerData>
    {
        public CustomerData Item1 { get => _item1; set => _item1 = value; }
        public CustomerData Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private CustomerData _item1;
        [SerializeField]
        private CustomerData _item2;

        public void Deconstruct(out CustomerData item1, out CustomerData item2) { item1 = Item1; item2 = Item2; }
    }
}
