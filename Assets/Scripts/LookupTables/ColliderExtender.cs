using Interactables.Triggers;
using Runtime.Automation;

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
            throw new System.NotImplementedException();
        }

        public void DisplayInEditor(string name)
        {
            throw new System.NotImplementedException();
        }
#endif
    }
}