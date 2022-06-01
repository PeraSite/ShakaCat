using UnityEngine;
using ShakaCat;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `CustomerDataPair`. Inherits from `AtomEvent&lt;CustomerDataPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/CustomerDataPair", fileName = "CustomerDataPairEvent")]
    public sealed class CustomerDataPairEvent : AtomEvent<CustomerDataPair>
    {
    }
}
