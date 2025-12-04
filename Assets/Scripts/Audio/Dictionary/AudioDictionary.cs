using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Audio.Dictionary
{
    public abstract class AudioDictionary<T> : ScriptableObject
    {
        [SerializeField] protected SerializedDictionary<T, AudioAggregate> clips = new();

        public bool TryGetClips(T id, out AudioAggregate audio) => clips.TryGetValue(id, out audio);
    }
}