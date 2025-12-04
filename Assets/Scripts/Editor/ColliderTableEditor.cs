using LookupTables;
using UnityEditor;
using Utilities;

namespace Editor
{
    [CustomEditor(typeof(ColliderTable))]
    public sealed class ColliderTableEditor : EditorBase
    {
        protected override void OnInspectorGUIInternal()
        {
            if (!EditorApplication.isPlaying) return;
            var local = (ColliderTable) target;
            using (new EditorGUI.DisabledScope(true))
                local.LookupTableEditor.DisplayDictionary("Lookup Table");
        }
    }
}