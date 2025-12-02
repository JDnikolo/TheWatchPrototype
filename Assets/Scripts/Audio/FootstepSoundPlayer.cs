using UnityEngine;
using Utilities;

namespace Audio
{
    public class FootstepSoundPlayer:MonoBehaviour
    {
        [SerializeField] private AudioSource footstepSource;
        [SerializeField] private ClipDictionary<PhysicMaterial> footstepSoundsDict;
        [SerializeField] private string terrainLayerName = "Terrain";

        private ClipAggregate m_currentClips;
        private int m_terrainLayerMask;

        private void Awake()
        {
            m_terrainLayerMask = LayerMask.GetMask(terrainLayerName);
        }

        public void PlayFootstep()
        {
            CheckFloorMaterial();
            if (footstepSource.isPlaying) footstepSource.Stop();
            var clip = m_currentClips?.Clips.GetRandom();
        
            if (!clip) return;
        
            footstepSource.clip = clip;
            m_currentClips?.Settings.Apply(footstepSource);
            footstepSource.Play();

        }
        private void CheckFloorMaterial()
        {
       
            if (!UnityEngine.Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2,
                    m_terrainLayerMask)) return;
            
            if (!footstepSoundsDict.TryGetClips(hit.collider.sharedMaterial, out m_currentClips))
            {
                //TODO: Default sounds in case no material is found?
            }
        }
    }
}
