using UnityEngine;

namespace Audio.Dictionary
{
    [CreateAssetMenu(fileName = "AudioFromMaterial", menuName = "Audio/Audio From Material")]
    public sealed class AudioFromMaterial : AudioDictionary<PhysicMaterial>
    {
    }
}