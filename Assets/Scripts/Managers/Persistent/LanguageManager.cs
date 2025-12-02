using System;
using System.Collections.Generic;
using Exceptions;
using Localization;
using Runtime;
using UnityEngine;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Language Manager")]
	public sealed class LanguageManager : Singleton<LanguageManager>
	{
		private HashSet<ILocalizationUpdatable> m_localizers = new();
		private LanguageEnum m_language = LanguageEnum.ENUM_LENGTH;
		
		protected override bool Override => false;

		public LanguageEnum Language
		{
			get
			{
				if (m_language >=  LanguageEnum.ENUM_LENGTH) throw new LoadFirstException();
				return m_language;
			}
			set
			{
				if (!Enum.IsDefined(typeof(LanguageEnum), value) || value == LanguageEnum.ENUM_LENGTH) 
					value = LanguageEnum.English;
				m_language = value;
				foreach (var localizer in m_localizers) localizer.OnLocalizationUpdate();
			}
		}

		public void AddLocalizer(ILocalizationUpdatable localizer) => m_localizers.Add(localizer);
		
		public void RemoveLocalizer(ILocalizationUpdatable localizer) => m_localizers.Remove(localizer);

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