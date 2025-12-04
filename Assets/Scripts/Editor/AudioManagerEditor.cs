using Managers.Persistent;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(AudioManager))]
    public sealed class AudioManagerEditor : EditorBase
    {
        public override bool RequiresConstantRepaint() => EditorApplication.isPlaying;

        protected override void OnInspectorGUIInternal()
        {
            if (!EditorApplication.isPlaying) return;
            var local = (AudioManager) target;
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.Toggle("Require Update", local.RequireUpdate);
                EditorGUILayout.FloatField("Fade In Time", local.FadeInTime);
                EditorGUILayout.FloatField("Fade In Timer", local.FadeInTimer);
                EditorGUILayout.FloatField("Fade In Volume", local.FadeInVolume);
                EditorGUILayout.FloatField("Fade Out Time", local.FadeOutTime);
                EditorGUILayout.FloatField("Fade Out Timer", local.FadeOutTimer);
                EditorGUILayout.FloatField("Fade Out Volume", local.FadeOutVolume);
                EditorGUILayout.Toggle("Delayed Fade", local.DelayedFade);
            }
        }
    }
}