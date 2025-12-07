using Attributes;
using Audio;
using Character;
using Localization.Text;
using UnityEngine;

namespace Localization.Speaker
{
    [CreateAssetMenu(fileName = "Speaker", menuName = "Localization/Speaker/Generic Speaker")]
    public sealed class SpeakerObject : TextObject
    {
        [CanBeNull, SerializeField] private CharacterProfileData profile;
        [CanBeNull, SerializeField] private AudioAggregate audio;
        
        public CharacterProfileData Profile => profile;
        
        public AudioAggregate Audio => audio;
    }
}