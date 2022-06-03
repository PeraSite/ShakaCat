#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using ShakaCat;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `DrinkDataPair`. Inherits from `AtomEventEditor&lt;DrinkDataPair, DrinkDataPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(DrinkDataPairEvent))]
    public sealed class DrinkDataPairEventEditor : AtomEventEditor<DrinkDataPair, DrinkDataPairEvent> { }
}
#endif
