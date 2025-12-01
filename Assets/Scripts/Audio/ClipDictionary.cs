using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Audio
{
    public abstract class ClipDictionary<T> : ScriptableObject
    {
        [SerializeField] protected SerializedDictionary<T, ClipAggregate> clips = new();

        public bool TryGetClips(T id, out ClipAggregate clip)
        {
            
            return clips.TryGetValue(id, out clip);
        }
    }
}