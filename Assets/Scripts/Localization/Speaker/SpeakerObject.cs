using Audio;
using Character;
using Localization.Text;
using UnityEngine;

namespace Localization.Speaker
{
    [CreateAssetMenu(fileName = "Speaker", menuName = "Localization/Speaker/Generic Speaker")]
    public class SpeakerObject : TextObject
    {
        [SerializeField] private string speaker;
        [SerializeField] private CharacterProfileData profile;
        [SerializeField] private ClipAggregate audio;
        
        public CharacterProfileData Profile => profile;
        
        public ClipAggregate Audio => audio;
    }
}