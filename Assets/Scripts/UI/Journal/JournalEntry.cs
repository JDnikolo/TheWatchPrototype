using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Journal
{
    [AddComponentMenu("UI/Journal/Journal Entry")]
    public sealed class JournalEntry : BaseBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI quoteText;
        [SerializeField] private ContentSizeFitter contentSizeFitter;
        
        public void SetNameText(string speakerName)
        {
            if (speakerName == "") nameText.gameObject.SetActive(false);
            nameText.text = speakerName;
            contentSizeFitter.enabled = true;
        }
        
        public void SetQuoteText(string speakerQuote) => quoteText.text = speakerQuote;
    }
}