using System;
using System.Collections.Generic;
using Boxing;
using Input;
using Localization;
using Runtime;
using UnityEngine;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Settings Manager")]
	public sealed class SettingsManager : Singleton<SettingsManager>
	{
		protected override bool Override => false;
		
		//Computer\HKEY_CURRENT_USER\Software\ExampleCompanyName\ExampleProductName

		private const string SETTINGS_LOADED = nameof(SETTINGS_LOADED);
		private const int SETTINGS_CORRECT = 1;
		
		public void Load()
		{
			if (GetInt(SETTINGS_LOADED) != SETTINGS_CORRECT) Save();
			AudioManager.Instance.AmbianceVolume = GetFloat(nameof(AudioManager.AmbianceVolume));
			AudioManager.Instance.EffectsVolume = GetFloat(nameof(AudioManager.EffectsVolume));
			AudioManager.Instance.MasterVolume = GetFloat(nameof(AudioManager.MasterVolume));
			AudioManager.Instance.MusicVolume = GetFloat(nameof(AudioManager.MusicVolume));
			AudioManager.Instance.SpeakerVolume = GetFloat(nameof(AudioManager.SpeakerVolume));
			var controlScheme = (ControlSchemeEnum) GetInt(nameof(InputManager.ControlScheme));
			if (!Enum.IsDefined(typeof(ControlSchemeEnum), controlScheme)) controlScheme = ControlSchemeEnum.Keyboard;
			InputManager.Instance.ControlScheme = controlScheme;
			InputManager.Instance.BindingOverridesJson = GetString(nameof(InputManager.Instance.BindingOverridesJson));
			var language = (LanguageEnum) GetInt(nameof(LanguageManager.Language));
			if (!Enum.IsDefined(typeof(LanguageEnum), language)) language = LanguageEnum.English;
			LanguageManager.Instance.Language = language;
		}

		public void Save()
		{
			SetInt(SETTINGS_LOADED, SETTINGS_CORRECT);
			SetFloat(nameof(AudioManager.AmbianceVolume), AudioManager.Instance.AmbianceVolume);
			SetFloat(nameof(AudioManager.EffectsVolume), AudioManager.Instance.EffectsVolume);
			SetFloat(nameof(AudioManager.MasterVolume), AudioManager.Instance.MasterVolume);
			SetFloat(nameof(AudioManager.MusicVolume), AudioManager.Instance.MusicVolume);
			SetFloat(nameof(AudioManager.SpeakerVolume), AudioManager.Instance.SpeakerVolume);
			SetInt(nameof(InputManager.ControlScheme), (int) InputManager.Instance.ControlScheme);
			SetInt(nameof(LanguageManager.Language), (int) LanguageManager.Instance.Language);
			PlayerPrefs.Save();
		}

		public void SetInt(string name, int value)
		{
#if UNITY_EDITOR
			UpdateValueType(name, value);
#endif
			PlayerPrefs.SetInt(name, value);
		}

		public void SetFloat(string name, float value)
		{
#if UNITY_EDITOR
			UpdateValueType(name, value);
#endif
			PlayerPrefs.SetFloat(name, value);
		}
		
		public void SetString(string name, string value)
		{
#if UNITY_EDITOR
			UpdateString(name, value);
#endif
			PlayerPrefs.SetString(name, value);
		}

		private int GetInt(string name)
		{
			var value = PlayerPrefs.GetInt(name);
#if UNITY_EDITOR
			UpdateValueType(name, value);
#endif
			return value;
		}

		private float GetFloat(string name)
		{
			var value = PlayerPrefs.GetFloat(name);
#if UNITY_EDITOR
			UpdateValueType(name, value);
#endif
			return value;
		}
		
		private string GetString(string name)
		{
			var value = PlayerPrefs.GetString(name);
#if UNITY_EDITOR
			UpdateString(name, value);
#endif
			return value;
		}
		
#if UNITY_EDITOR
		public Dictionary<string, object> SavedValues { get; } = new();

		private void UpdateValueType<T>(string name, T value) where T : struct
		{
			MutableRef<T> reference;
			if (!SavedValues.TryGetValue(name, out var savedValue))
			{
				reference = new MutableRef<T>();
				SavedValues[name] = reference;
			}
			else reference = (MutableRef<T>) savedValue;

			reference.Value = value;
		}

		private void UpdateString(string name, string value) => SavedValues[name] = value;
#endif
	}
}