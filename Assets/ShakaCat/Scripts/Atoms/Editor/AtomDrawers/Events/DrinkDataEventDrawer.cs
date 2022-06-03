#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `DrinkData`. Inherits from `AtomDrawer&lt;DrinkDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(DrinkDataEvent))]
    public class DrinkDataEventDrawer : AtomDrawer<DrinkDataEvent> { }
}
#endif
