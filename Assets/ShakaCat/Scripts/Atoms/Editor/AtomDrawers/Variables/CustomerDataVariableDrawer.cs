#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `CustomerData`. Inherits from `AtomDrawer&lt;CustomerDataVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CustomerDataVariable))]
    public class CustomerDataVariableDrawer : VariableDrawer<CustomerDataVariable> { }
}
#endif
