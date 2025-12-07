using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Audio.Dictionary
{
    public abstract class AudioDictionary<T> : BaseObject
    {
        [SerializeField] private SerializedDictionary<T, AudioAggregate> clips = new();
        
        protected SerializedDictionary<T, AudioAggregate> Clips => clips;
        
        public bool TryGetClips(T id, out AudioAggregate audio) => clips.TryGetValue(id, out audio);
    }
}