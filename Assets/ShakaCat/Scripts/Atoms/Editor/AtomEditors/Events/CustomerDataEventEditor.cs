#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using ShakaCat;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CustomerData`. Inherits from `AtomEventEditor&lt;CustomerData, CustomerDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CustomerDataEvent))]
    public sealed class CustomerDataEventEditor : AtomEventEditor<CustomerData, CustomerDataEvent> { }
}
#endif
