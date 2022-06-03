using System;
using UnityEngine;
using ShakaCat;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;DrinkData&gt;`. Inherits from `IPair&lt;DrinkData&gt;`.
    /// </summary>
    [Serializable]
    public struct DrinkDataPair : IPair<DrinkData>
    {
        public DrinkData Item1 { get => _item1; set => _item1 = value; }
        public DrinkData Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private DrinkData _item1;
        [SerializeField]
        private DrinkData _item2;

        public void Deconstruct(out DrinkData item1, out DrinkData item2) { item1 = Item1; item2 = Item2; }
    }
}