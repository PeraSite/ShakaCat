#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using ShakaCat;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CustomerDataPair`. Inherits from `AtomEventEditor&lt;CustomerDataPair, CustomerDataPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CustomerDataPairEvent))]
    public sealed class CustomerDataPairEventEditor : AtomEventEditor<CustomerDataPair, CustomerDataPairEvent> { }
}
#endif
