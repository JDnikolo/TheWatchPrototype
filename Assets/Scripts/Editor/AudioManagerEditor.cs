using Audio;
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
                EditorGUILayout.LabelField("Music");
                EditorGUILayout.FloatField("Music Fade In Time", local.MusicFadeInTime);
                EditorGUILayout.FloatField("Music Fade In Timer", local.MusicFadeInTimer);
                EditorGUILayout.FloatField("Music Fade In Volume", local.MusicFadeInVolume);
                EditorGUILayout.FloatField("Music Fade Out Time", local.MusicFadeOutTime);
                EditorGUILayout.FloatField("Music Fade Out Timer", local.MusicFadeOutTimer);
                EditorGUILayout.FloatField("Music Fade Out Volume", local.MusicFadeOutVolume);
                EditorGUILayout.Toggle("Music Delayed Fade", local.MusicDelayedFade);
                EditorGUILayout.LabelField("Snapshot");
                EditorGUILayout.ObjectField("Current Snapshot", local.CurrentSnapshot, typeof(AudioSnapshot), false);
                EditorGUILayout.ObjectField("Fade Snapshot", local.FadeSnapshotEditor, typeof(AudioSnapshot), false);
                EditorGUILayout.FloatField("Snapshot Fade In Time", local.SnapshotFadeInTime);
                EditorGUILayout.FloatField("Snapshot Fade In Timer", local.SnapshotFadeInTimer);
                EditorGUILayout.FloatField("Snapshot Fade Out Time", local.SnapshotFadeOutTime);
                EditorGUILayout.FloatField("Snapshot Fade Out Timer", local.SnapshotFadeOutTimer);
                EditorGUILayout.EnumPopup("Snapshot Fade Mode", local.SnapshotFadeModeEditor);
            }
        }
    }
}