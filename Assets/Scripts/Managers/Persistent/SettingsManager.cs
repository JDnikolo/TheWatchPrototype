using System;
using System.IO;
using Cfg;
using Input;
using Localization;
using Runtime;
using UnityEngine;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Settings Manager")]
	public sealed class SettingsManager : Singleton<SettingsManager>
	{
		private const string AudioSection = "Audio";
		private const string ControlsSection = "Controls";
		private const string GameplaySection = "Gameplay";
		private const string GraphicsSection = "Graphics";

		protected override bool Override => false;

		public static string FilePath => Path.Combine(Application.persistentDataPath, "Settings.cfg");

		public void Load()
		{
			var filePath = FilePath;
			if (!File.Exists(filePath)) Save();
			using (var reader = new ConfigReader(File.OpenText(filePath)))
				while (reader.TryRead(out var output))
				{
					if (output.IsComment) continue;
					switch (output.Section)
					{
						case AudioSection:
							float floatVal;
							switch (output.Key)
							{
								case nameof(AudioManager.AmbianceVolume):
									if (float.TryParse(output.Value, out floatVal))
										AudioManager.Instance.AmbianceVolume = floatVal;
									break;
								case nameof(AudioManager.EffectsVolume):
									if (float.TryParse(output.Value, out floatVal))
										AudioManager.Instance.EffectsVolume = floatVal;
									break;
								case nameof(AudioManager.MasterVolume):
									if (float.TryParse(output.Value, out floatVal))
										AudioManager.Instance.MasterVolume = floatVal;
									break;
								case nameof(AudioManager.MusicVolume):
									if (float.TryParse(output.Value, out floatVal))
										AudioManager.Instance.MusicVolume = floatVal;
									break;
								case nameof(AudioManager.SpeakerVolume):
									if (float.TryParse(output.Value, out floatVal))
										AudioManager.Instance.SpeakerVolume = floatVal;
									break;
							}

							break;
						case ControlsSection:
							switch (output.Key)
							{
								case nameof(InputManager.ControlScheme):
									if (Enum.TryParse(output.Value, out ControlScheme currentScheme))
										InputManager.Instance.ControlScheme = currentScheme;
									break;
							}

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
						case GraphicsSection:
							break;
					}
				}

		}

		public void Save()
		{
			using (var writer = new ConfigWriter(File.CreateText(FilePath)))
			{
				writer.WriteSection(AudioSection);
				writer.Write(nameof(AudioManager.AmbianceVolume), AudioManager.Instance.AmbianceVolume);
				writer.Write(nameof(AudioManager.EffectsVolume), AudioManager.Instance.EffectsVolume);
				writer.Write(nameof(AudioManager.MasterVolume), AudioManager.Instance.MasterVolume);
				writer.Write(nameof(AudioManager.MusicVolume), AudioManager.Instance.MusicVolume);
				writer.Write(nameof(AudioManager.SpeakerVolume), AudioManager.Instance.SpeakerVolume);
				writer.WriteSection(ControlsSection);
				writer.Write(nameof(InputManager.ControlScheme), InputManager.Instance.ControlScheme);
				writer.WriteSection(GameplaySection);
				writer.Write(nameof(LanguageManager.CurrentLanguage), LanguageManager.Instance.CurrentLanguage);
				writer.WriteSection(GraphicsSection);
			}
		}
	}
}