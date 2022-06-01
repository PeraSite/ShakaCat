#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CustomerData`. Inherits from `AtomDrawer&lt;CustomerDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CustomerDataEvent))]
    public class CustomerDataEventDrawer : AtomDrawer<CustomerDataEvent> { }
}
#endif
