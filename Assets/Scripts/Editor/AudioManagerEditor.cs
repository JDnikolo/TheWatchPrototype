using Managers.Persistent;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(AudioManager))]
    public sealed class AudioManagerEditor : EditorBase
    {
        protected override void OnInspectorGUIInternal()
        {
            if (!EditorApplication.isPlaying) return;
            var local = (AudioManager) target;
        }
    }
}