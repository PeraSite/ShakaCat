using UnityEngine;
using ShakaCat;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `DrinkData`. Inherits from `AtomEvent&lt;DrinkData&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/DrinkData", fileName = "DrinkDataEvent")]
    public sealed class DrinkDataEvent : AtomEvent<DrinkData>
    {
    }
}
