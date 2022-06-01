﻿#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CustomerDataPair`. Inherits from `AtomDrawer&lt;CustomerDataPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CustomerDataPairEvent))]
    public class CustomerDataPairEventDrawer : AtomDrawer<CustomerDataPairEvent> { }
}
#endif
