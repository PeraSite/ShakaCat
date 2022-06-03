#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `DrinkData`. Inherits from `AtomDrawer&lt;DrinkDataVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(DrinkDataVariable))]
    public class DrinkDataVariableDrawer : VariableDrawer<DrinkDataVariable> { }
}
#endif
