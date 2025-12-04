using Audio;

namespace Callbacks.Audio
{
    public interface IAudioGroupVolumeChanged
    {
        void OnAudioGroupVolumeChanged(AudioGroup group);
    }
}