using Audio;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(AudioGroup))]
    public sealed class AudioGroupEditor : EditorBase
    {
        protected override void OnInspectorGUIInternal()
        {
            if (!EditorApplication.isPlaying) return;
            var local = (AudioGroup) target;
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.FloatField("Volume Override", local.ThisOverride);
                EditorGUILayout.FloatField("Parent Override", local.ParentOverride);
                EditorGUILayout.FloatField("Result Override", local.VolumeOverride);
            }
        }
    }
}