using System.Collections.Generic;
using Localization;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Language Manager")]
	public sealed class LanguageManager : Singleton<LanguageManager>
	{
		private HashSet<IOnLocalizationUpdate> m_localizers = new();
		
		protected override bool Override => false;
		
		public LanguageEnum CurrentLanguage { get; private set; }

		public void AddLocalizer(IOnLocalizationUpdate localizer) => m_localizers.Add(localizer);
		
		public void RemoveLocalizer(IOnLocalizationUpdate localizer) => m_localizers.Remove(localizer);
		
		public void SetNewLanguage(LanguageEnum language)
		{
			CurrentLanguage = language;
			foreach (var localizer in m_localizers) localizer.OnLocalizationUpdate();
		}
	}
}