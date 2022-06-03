using UnityEngine;
using System;
using ShakaCat;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `DrinkData`. Inherits from `AtomVariable&lt;DrinkData, DrinkDataPair, DrinkDataEvent, DrinkDataPairEvent, DrinkDataDrinkDataFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/DrinkData", fileName = "DrinkDataVariable")]
    public sealed class DrinkDataVariable : AtomVariable<DrinkData, DrinkDataPair, DrinkDataEvent, DrinkDataPairEvent, DrinkDataDrinkDataFunction>
    {
        protected override bool ValueEquals(DrinkData other) {
            return _value == other;
        }
    }
}
