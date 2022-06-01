using UnityEditor;
using UnityAtoms.Editor;
using ShakaCat;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `CustomerData`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(CustomerDataVariable))]
    public sealed class CustomerDataVariableEditor : AtomVariableEditor<CustomerData, CustomerDataPair> { }
}
