using UnityEngine;
using ShakaCat;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `IngredientData`. Inherits from `AtomValueList&lt;IngredientData, IngredientDataEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/IngredientData", fileName = "IngredientDataValueList")]
    public sealed class IngredientDataValueList : AtomValueList<IngredientData, IngredientDataEvent> { }
}
