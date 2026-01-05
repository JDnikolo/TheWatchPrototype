using Attributes;
using Audio;
using Character;
using Localization.Text;
using Logic.Boolean;
using UnityEngine;

namespace Localization.Speaker
{
    [CreateAssetMenu(fileName = "Speaker", menuName = "Localization/Speaker/Generic Speaker")]
    public sealed class SpeakerObject : TextObject
    {
        [CanBeNull, SerializeField] private CharacterProfileData profile;
        [CanBeNull, SerializeField] private AudioAggregate audio;
        [CanBeNull, SerializeField] private LogicBoolean allowSkip;
        
        public CharacterProfileData Profile => profile;
        
        public AudioAggregate Audio => audio;
        
        public LogicBoolean AllowSkip => allowSkip;
    }
}