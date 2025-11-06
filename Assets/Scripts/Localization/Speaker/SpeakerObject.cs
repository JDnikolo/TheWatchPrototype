using Localization.Text;
using UnityEngine;

namespace Localization.Speaker
{
    [CreateAssetMenu(fileName = "Speaker", menuName = "Localization/Speaker/Speaker")]
    public class SpeakerObject : TextObject
    {
        [SerializeField] private string speaker;
        
        public string Speaker => speaker;
    }
}