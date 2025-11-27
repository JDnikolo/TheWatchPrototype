using Localization;
using Localization.Text;
using Managers.Persistent;
using Runtime;
using UI.Text;
using UnityEditor;
using UnityEngine;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Label")]
	public sealed class Label : MonoBehaviour, IPrewarm, ILocalizationUpdatable
	{
		[SerializeField] private TextWriter textWriter;
		[SerializeField] [HideInInspector] private TextObject textToDisplay;

		private TextObject m_textToDisplay;
		private bool m_firstDone;

		public void SetTextToDisplay(TextObject textToDisplay)
		{
			m_textToDisplay = textToDisplay;
			OnLocalizationUpdate();
		}

		public void OnLocalizationUpdate() => textWriter.WriteText(textToDisplay.Text);

		public void OnPrewarm()
		{
			m_textToDisplay = textToDisplay;
			var languageManager = LanguageManager.Instance;
			if (languageManager) languageManager.AddLocalizer(this);
		}
		
		private void OnEnable()
		{
			if (!m_firstDone)
			{
				m_firstDone = true;
				GameManager.Instance.InvokeOnNextFrameUpdate(DelayedUpdate);
			}
			else OnLocalizationUpdate();
		}

		private void DelayedUpdate() => OnLocalizationUpdate();

		private void OnDestroy()
		{
			var languageManager = LanguageManager.Instance;
			if (languageManager) languageManager.RemoveLocalizer(this);
		}
#if UNITY_EDITOR
		public TextObject TextToDisplay => m_textToDisplay;
		
		private void OnValidate()
		{
			var text = textToDisplay;
			if (text)
			{
				var obj = gameObject;
				if (obj.name != text.name)
				{
					obj.name = text.name;
					EditorUtility.SetDirty(obj);
				}
			}
		}
#endif
	}
}