using System.Text;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
	public class TextWriter : MonoBehaviour
	{
		[Header("Linked behaviors")] 
		[SerializeField] private TextMeshProUGUI textWriter;
		[SerializeField] private Slider slider;

		[Header("Control schemes")] 
		[SerializeField] private string skipActionName = "SkipDialogue";

		[Header("Delays")] 
		[SerializeField] private float characterDelay;
		[SerializeField] private float periodDelay;
		[SerializeField] private float commaDelay;
		[SerializeField] private float spaceDelay;

		private TextWriterInput m_input;
		private InputAction m_skipAction;
		private StringBuilder m_stringBuilder = new();
		private float m_timer;
		private int m_previousPageCount;
		private int m_seek;
		private bool m_write;
		private bool m_forceFinish;

		public void WriteText(TextWriterInput textWriterInput)
		{
			m_input = textWriterInput;
			StartWriting();
		}

		public void DisposeText()
		{
			m_input = TextWriterInput.Empty;
			StopWriting();
		}
	
		private void Start()
		{
			m_skipAction = InputManager.Instance.GetUIAction(skipActionName);
			slider.onValueChanged.AddListener(SliderChanged);
			FullReset();
		}

		private void OnEnable() => FullReset();

		private void OnDisable() => StopWriting();

		private void SliderChanged(float value) => textWriter.pageToDisplay = Mathf.RoundToInt(value) + 1;

		private void Update()
		{
			var skipDialogue = ShouldSkipDialogue();
			if (m_forceFinish)
			{
				m_forceFinish = false;
				CheckPages();
			}

			if (!m_write)
			{
				if (skipDialogue)
				{
					if (m_input.OnTextWriterFinished != null) m_input.OnTextWriterFinished.OnTextWriterFinished(this);
					else
					{
						DialogueManager.Instance.CloseDialogue();
						InputManager.Instance.ForcePlayerInput();
					}
				}

				return;
			}

			var changed = false;
			var text = m_input.TextToDisplay.Text;
			if (skipDialogue)
			{
				m_forceFinish = true;
				m_write = false;
				changed = true;
				m_stringBuilder.Append(text.Substring(m_seek));
			}
			else
			{
				m_timer -= Time.deltaTime;
				while (m_timer < 0)
				{
					var seek = m_seek++;
					if (seek >= text.Length)
					{
						m_write = false;
						break;
					}

					changed = true;
					var character = text[seek];
					switch (character)
					{
						case '.':
							m_timer += periodDelay;
							break;
						case ' ':
							m_timer += spaceDelay;
							break;
						case ',':
							m_timer += commaDelay;
							break;
						default:
							m_timer += characterDelay;
							break;
					}

					m_stringBuilder.Append(character);
				}
			}

			if (changed)
			{
				textWriter.SetText(m_stringBuilder);
				CheckPages();
			}

			if (!m_write) ResetInternals();
		}
		
		private bool ShouldSkipDialogue() => m_skipAction.WasPressedThisFrame();

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
			if (pageCount < 2)
			{
				slider.maxValue = 1;
				slider.enabled = false;
			}
			else
			{
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