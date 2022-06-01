#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `IngredientData`. Inherits from `AtomDrawer&lt;IngredientDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(IngredientDataEvent))]
    public class IngredientDataEventDrawer : AtomDrawer<IngredientDataEvent> { }
}
#endif
