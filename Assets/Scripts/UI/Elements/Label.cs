using Attributes;
using Callbacks.Localization;
using Callbacks.Prewarm;
using Localization.Text;
using Managers.Persistent;
using UI.Text;
using UnityEngine;
using Utilities;

namespace UI.Elements
{
	[AddComponentMenu("UI/Elements/Label")]
	public sealed class Label : BaseBehaviour, IPrewarm, ILocalizationUpdatable
	{
		[SerializeField, AutoAssigned(AssignMode.Self, typeof(TextWriter))] 
		private TextWriter textWriter;

		[CanBeNullInPrefab, SerializeField, HideInInspector]
		private TextObject textToDisplay;

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

		public void SetManagedTextToDisplay(TextObject newTextToDisplay) =>
			this.DirtyReplaceObject(ref textToDisplay, newTextToDisplay);
		
		private void OnValidate() => this.UpdateNameTo(textToDisplay);
#endif
	}
}