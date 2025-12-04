using Audio;
using Runtime.Automation;
using UnityEditor;
using Utilities;

namespace Managers.Persistent
{
    public sealed partial class AudioManager
    {
        public struct State
#if UNITY_EDITOR
            : IEditorDisplayable
#endif
        {
            public AudioSnapshot CurrentSnapshot;
            public bool DelayedFade;
            public float FadeInTime;
            public float FadeOutTime;
#if UNITY_EDITOR
            public void DisplayInEditor()
            {
                EditorGUILayout.ObjectField("Current Snapshot", CurrentSnapshot, typeof(AudioSnapshot), false);
                EditorGUILayout.Toggle("Delayed Fade", DelayedFade);
                EditorGUILayout.FloatField("Fade In Time", FadeInTime);
                EditorGUILayout.FloatField("Fade Out Time", FadeOutTime);
            }
			
            public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
        }
    }
}