using System.Collections.Generic;
using Localization;
using Runtime;
using UnityEngine;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Language Manager")]
	public sealed class LanguageManager : Singleton<LanguageManager>
	{
		private HashSet<ILocalizationUpdatable> m_localizers = new();
		
		protected override bool Override => false;
		
		public LanguageEnum CurrentLanguage { get; private set; }

		public void Clear() => m_localizers.Clear();
		
		public void AddLocalizer(ILocalizationUpdatable localizer) => m_localizers.Add(localizer);
		
		public void RemoveLocalizer(ILocalizationUpdatable localizer) => m_localizers.Remove(localizer);
		
		public void SetNewLanguage(LanguageEnum language)
		{
			CurrentLanguage = language;
			foreach (var localizer in m_localizers) localizer.OnLocalizationUpdate();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_localizers.Clear();
			m_localizers = null;
		}
#if UNITY_EDITOR
		public HashSet<ILocalizationUpdatable> Localizers => m_localizers;
#endif
	}
}