using UnityEditor;
using UnityAtoms.Editor;
using ShakaCat;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `DrinkData`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(DrinkDataVariable))]
    public sealed class DrinkDataVariableEditor : AtomVariableEditor<DrinkData, DrinkDataPair> { }
}
