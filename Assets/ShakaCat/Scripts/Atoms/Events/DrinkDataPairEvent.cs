using UnityEngine;
using ShakaCat;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `DrinkDataPair`. Inherits from `AtomEvent&lt;DrinkDataPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/DrinkDataPair", fileName = "DrinkDataPairEvent")]
    public sealed class DrinkDataPairEvent : AtomEvent<DrinkDataPair>
    {
    }
}
