#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using ShakaCat;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `IngredientData`. Inherits from `AtomEventEditor&lt;IngredientData, IngredientDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(IngredientDataEvent))]
    public sealed class IngredientDataEventEditor : AtomEventEditor<IngredientData, IngredientDataEvent> { }
}
#endif
