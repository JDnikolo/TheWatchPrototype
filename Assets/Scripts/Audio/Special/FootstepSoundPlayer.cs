using Audio.Dictionary;
using UnityEngine;
using Utilities;

namespace Audio.Special
{
    public sealed class FootstepSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource footstepSource;
        [SerializeField] private AudioDictionary<PhysicMaterial> footstepSoundsDict;
        [SerializeField] private LayerMask terrainLayerMask;

        private AudioAggregate m_currentAudios;

        public void PlayFootstep()
        {
            if (!UnityEngine.Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2, terrainLayerMask)) return;
            if (!footstepSoundsDict.TryGetClips(hit.collider.sharedMaterial, out m_currentAudios))
            {
                //TODO: Default sounds in case no material is found?
                m_currentAudios = null;
            }
            
            if (footstepSource.isPlaying) footstepSource.Stop();
            if (!m_currentAudios) return;
            footstepSource.clip = m_currentAudios.Clips.GetRandom();
            m_currentAudios.Settings.Apply(footstepSource, m_currentAudios.Group);
            footstepSource.Play();
        }
    }
}