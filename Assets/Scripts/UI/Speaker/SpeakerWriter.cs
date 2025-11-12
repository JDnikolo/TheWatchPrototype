using System.Text;
using Audio;
using Callbacks.Text;
using Managers;
using TMPro;
using UI.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities;

namespace UI.Speaker
{
	[AddComponentMenu("UI/Speaker/Speaker Writer")]
	public sealed class SpeakerWriter : MonoBehaviour, IFrameUpdatable
	{
		[Header("Linked behaviors")] 
		[SerializeField] private TextMeshProUGUI textWriter;
		[SerializeField] private TextWithBackground speakerWriter;
		[SerializeField] private AudioSource speakerSource;
		[SerializeField] private Slider slider;

		[Header("Control schemes")] 
		[SerializeField] private string skipActionName = "SkipDialogue";

		[Header("Delays")] 
		[SerializeField] private float characterDelay;
		[SerializeField] private float periodDelay;
		[SerializeField] private float commaDelay;
		[SerializeField] private float spaceDelay;

		private readonly StringBuilder m_stringBuilder = new();
		
		private ISpeakerWriterFinished m_onFinished;
		private InputAction m_skipAction;
		private ClipAggregate m_audio;
		private string m_text;
		private float m_timer;
		private int m_previousPageCount;
		private int m_seek;
		private bool m_write;
		private bool m_skipText;
		private bool m_forceFinish;
		private bool m_startNewAudio;

		public byte UpdateOrder => 0;

		private InputAction SkipAction
		{
			get
			{
				if (m_skipAction == null)
				{
					m_skipAction = InputManager.Instance.GetUIAction(skipActionName);
					slider.onValueChanged.AddListener(SliderChanged);
				}

				return m_skipAction;
			}
		}

		public void WriteText(SpeakerWriterInput input)
		{
			StartWriting();
			m_onFinished = input.OnTextWriterFinished;
			var profile = input.TextToDisplay.Profile;
			if (profile) speakerWriter.WriteText(profile.CharacterName);
			else speakerWriter.WriteText(null);
			m_text = input.TextToDisplay.Text;
			m_audio = input.TextToDisplay.Audio;
		}

		public void DisposeText() => StopWriting();

		public void SkipText() => m_skipText = true;

		private void SliderChanged(float value) => textWriter.pageToDisplay = Mathf.RoundToInt(value) + 1;

		public void OnFrameUpdate()
		{
			var skipDialogue = SkipAction.WasPressedThisFrame();
			if (m_skipText)
			{
				m_skipText = false;
				skipDialogue = true;
			}

			if (m_forceFinish)
			{
				m_forceFinish = false;
				CheckPages();
			}

			if (!m_write)
			{
				if (skipDialogue)
				{
					if (m_onFinished != null)
					{
						var onFinished = m_onFinished;
						m_onFinished = null;
						onFinished.OnTextWriterFinished(this);
					}
					else
					{
						UIManager.Instance.CloseTextWriter();
						InputManager.Instance.ForcePlayerInput();
					}
				}

				return;
			}

			var changed = false;
			if (skipDialogue)
			{
				m_forceFinish = true;
				m_write = false;
				changed = true;
				m_stringBuilder.Append(m_text.Substring(m_seek));
				speakerSource.Stop();
			}
			else
			{
				if (m_startNewAudio)
				{
					if (speakerSource.isPlaying) goto Skip;
					m_startNewAudio = false;
					StartNextSpeaker();
				}

				var deltaTime = Time.deltaTime;
				m_timer -= deltaTime;
				while (m_timer < 0)
				{
					var seek = m_seek++;
					if (seek >= m_text.Length)
					{
						m_write = false;
						break;
					}

					changed = true;
					var character = m_text[seek];
					m_timer += GetDelay(character);
					if (character == '.') m_startNewAudio = true;
					m_stringBuilder.Append(character);
				}
				
				Skip: ;
			}

			if (changed)
			{
				if (!m_startNewAudio && !speakerSource.isPlaying) StartNextSpeaker();
				textWriter.SetText(m_stringBuilder);
				CheckPages();
			}

			if (!m_write) ResetInternals();
		}

		private void StartNextSpeaker()
		{
			if (!m_audio) return;
			//var speakerTime = CalculateSpeakerTime();
			var clip = m_audio.Clips.GetRandom();
			speakerSource.clip = clip;
			m_audio.Settings.Apply(speakerSource);
			speakerSource.Play();
		}

		private float CalculateSpeakerTime()
		{
			var delay = 0f;
			for (var i = m_seek; i < m_text.Length; i++)
			{
				var character = m_text[i];
				delay += GetDelay(character);
				if (character  == '.') break;
			}

			return delay;
		}
		
		private float GetDelay(char character)
		{
			switch (character)
			{
				case '.':
					return periodDelay;
				case ' ':
					return spaceDelay;
				case ',':
					return commaDelay;
				default:
					return characterDelay;
			}
		}

		private void CheckPages()
		{
			var textInfo = textWriter.textInfo;
			if (textInfo != null)
			{
				var pageCount = textWriter.textInfo.pageCount;
				if (pageCount > m_previousPageCount)
				{
					m_previousPageCount = pageCount;
					textWriter.pageToDisplay = pageCount;
					SetSlider(pageCount);
				}
			}
		}

		private void SetSlider(int pageCount)
		{
			if (pageCount < 2) slider.gameObject.SetActive(false);
			else
			{
				slider.gameObject.SetActive(true);
				slider.maxValue = pageCount - 1;
				slider.enabled = true;
				slider.SetValueWithoutNotify(slider.maxValue);
			}
		}

		private void StartWriting()
		{
			FullReset();
			m_write = true;
		}

		private void StopWriting()
		{
			FullReset();
			m_write = false;
		}

		private void FullReset()
		{
			m_onFinished = null;
			m_audio = null;
			ResetInternals();
			ResetSlider();
			ResetWriter();
		}

		private void ResetInternals()
		{
			m_stringBuilder.Clear();
			m_previousPageCount = 0;
			m_timer = 0;
			m_seek = 0;
			m_text = null;
			m_startNewAudio = true;
		}

		private void ResetWriter()
		{
			textWriter.SetText(m_stringBuilder);
			textWriter.pageToDisplay = 1;
		}

		private void ResetSlider()
		{
			SetSlider(0);
			slider.SetValueWithoutNotify(0f);
		}
	}
}