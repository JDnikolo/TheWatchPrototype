using LookupTables;
using UnityEditor;
using Utilities;

namespace Editor
{
    [CustomEditor(typeof(RigidBodyTable))]
    public sealed class RigidBodyTableEditor : EditorBase
    {
        protected override void OnInspectorGUIInternal()
        {
            if (!EditorApplication.isPlaying) return;
            var local = (RigidBodyTable) target;
            using (new EditorGUI.DisabledScope(true))
                local.LookupTableEditor.DisplayDictionary("Lookup Table");
        }
    }
}