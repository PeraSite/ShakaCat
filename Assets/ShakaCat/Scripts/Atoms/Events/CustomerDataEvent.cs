using UnityEngine;
using ShakaCat;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `CustomerData`. Inherits from `AtomEvent&lt;CustomerData&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/CustomerData", fileName = "CustomerDataEvent")]
    public sealed class CustomerDataEvent : AtomEvent<CustomerData>
    {
    }
}
