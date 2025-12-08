using Audio.Dictionary;
using UnityEngine;

namespace Audio.Special
{
	public sealed class FootstepSoundPlayer : BaseBehaviour
	{
		[SerializeField] private AudioPlayer player;
		[SerializeField] private AudioDictionary<PhysicMaterial> footstepSoundsDict;
		[SerializeField] private LayerMask terrainLayerMask;

		private AudioAggregate m_currentAudios;

		public void PlayFootstep()
		{
			if (!UnityEngine.Physics.Raycast(transform.position,
					Vector3.down, out var hit, 2, terrainLayerMask)) return;
			if (!hit.collider.sharedMaterial) return;
			if (!footstepSoundsDict.TryGetClips(hit.collider.sharedMaterial,
					out m_currentAudios)) m_currentAudios = null;
			if (player.IsPlaying) player.Stop();
			if (!m_currentAudios) return;
			player.Play(m_currentAudios);
		}
	}
}