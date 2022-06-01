using UnityEngine;
using ShakaCat;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `IngredientData`. Inherits from `AtomEvent&lt;IngredientData&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/IngredientData", fileName = "IngredientDataEvent")]
    public sealed class IngredientDataEvent : AtomEvent<IngredientData>
    {
    }
}
