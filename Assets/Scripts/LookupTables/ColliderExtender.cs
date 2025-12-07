using Interactables.Triggers;
using Runtime.Automation;
using UnityEditor;
using Utilities;

namespace LookupTables
{
    public struct ColliderExtender
#if UNITY_EDITOR
        : IEditorDisplayable
#endif
    {
        public InteractableShoutTrigger ShoutTrigger;
#if UNITY_EDITOR
        public void DisplayInEditor()
        {
            EditorGUILayout.ObjectField(ShoutTrigger, typeof(InteractableShoutTrigger), false);
        }

        public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
    }
}