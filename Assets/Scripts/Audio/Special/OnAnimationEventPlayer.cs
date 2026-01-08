using UnityEngine;

namespace Audio.Special
{
    public class OnAnimationEventPlayer : BaseBehaviour
    {
        [SerializeField] private AudioPlayer player;
        [SerializeField] private AudioAggregate clipsToPlay;
        
        public void OnAnimationEvent()
        {
            if (player.IsPlaying) return;
            if (!clipsToPlay) return;
            player.Play(clipsToPlay);
        }
    }
    
}