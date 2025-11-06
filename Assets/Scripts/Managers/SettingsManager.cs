using System;
using System.IO;
using UnityEngine;
using Cfg;
using Localization;

namespace Managers
{
	[AddComponentMenu("Managers/Settings Manager")]
	public sealed class SettingsManager : Singleton<SettingsManager>
	{
		private const string AudioSection = "Audio";
		private const string GameplaySection = "Gameplay";
		private const string InputSection = "Input";
		
		protected override bool Override => false;
		
		private static string FilePath => Path.Combine(Application.persistentDataPath, "Settings.cfg");
		
		public void Load()
		{
			var filePath = FilePath;
			if (!File.Exists(filePath)) return;
			using (var reader = new ConfigReader(File.OpenText(filePath)))
				while (reader.TryRead(out var output))
				{
					if (output.IsComment) continue;
					switch (output.Section)
					{
						case AudioSection:
							break;
						case GameplaySection:
							switch (output.Key)
							{
								case nameof(LanguageManager.CurrentLanguage):
									if (Enum.TryParse(output.Value, out LanguageEnum currentLanguage)) 
										LanguageManager.Instance.SetNewLanguage(currentLanguage);
									break;
							}
							break;
						case InputSection:
							break;
					}
				}

		}

		public void Save()
		{
			using (var writer = new ConfigWriter(File.CreateText(FilePath)))
			{
				writer.WriteSection(AudioSection);
				writer.WriteSection(GameplaySection);
				writer.Write(nameof(LanguageManager.CurrentLanguage), 
					LanguageManager.Instance.CurrentLanguage);
				writer.WriteSection(InputSection);
			}
		}
	}
}