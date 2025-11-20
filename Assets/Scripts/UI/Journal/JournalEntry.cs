using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Journal
{
    public class JournalEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI quoteText;
        [SerializeReference] private ContentSizeFitter contentSizeFitter;
        
        public void SetNameText(string speakerName)
        {
            if (speakerName == "")
            {
                nameText.gameObject.SetActive(false);
            }
            nameText.text = speakerName;
            contentSizeFitter.enabled = true;
        }
        
        public void SetQuoteText(string speakerQuote) => quoteText.text = speakerQuote;
    }
}