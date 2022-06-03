#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using ShakaCat;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `DrinkData`. Inherits from `AtomEventEditor&lt;DrinkData, DrinkDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(DrinkDataEvent))]
    public sealed class DrinkDataEventEditor : AtomEventEditor<DrinkData, DrinkDataEvent> { }
}
#endif
