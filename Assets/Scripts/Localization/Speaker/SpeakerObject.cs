using Character;
using Localization.Text;
using UnityEngine;

namespace Localization.Speaker
{
    [CreateAssetMenu(fileName = "Speaker", menuName = "Localization/Speaker/Speaker")]
    public class SpeakerObject : TextObject
    {
        [SerializeField] private string speaker;
        [SerializeField] private CharacterProfileData profile;
        
        public CharacterProfileData Profile => profile;
    }
}