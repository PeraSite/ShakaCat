#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `IngredientData`. Inherits from `AtomDrawer&lt;IngredientDataValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(IngredientDataValueList))]
    public class IngredientDataValueListDrawer : AtomDrawer<IngredientDataValueList> { }
}
#endif
