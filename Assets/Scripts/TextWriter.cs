using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
	[Header("Linked behaviors")] [SerializeField]
	private TextMeshProUGUI textWriter;

	[SerializeField] private TextAsset textAsset;
	[SerializeField] private Slider slider;

	[Header("Control schemes")] [SerializeField]
	private InputActionAsset actionAsset;
	
	[SerializeField] private string uiMapName = "UI";
	[SerializeField] private string skipActionName = "SkipDialogue";

	[Header("Delays")] [SerializeField] private float characterDelay;
	[SerializeField] private float periodDelay;
	[SerializeField] private float commaDelay;
	[SerializeField] private float spaceDelay;

	private InputAction m_skipAction;
	private StringBuilder m_stringBuilder = new();
	private float m_timer;
	private int m_previousPageCount;
	private int m_seek;
	private bool m_write;
	private bool m_forceFinish;

	private void Start()
	{
		m_skipAction = actionAsset.FindActionMap(uiMapName).FindAction(skipActionName);
		slider.onValueChanged.AddListener(SliderChanged);
		StartWriting();
	}

	private void SliderChanged(float value) => textWriter.pageToDisplay = Mathf.RoundToInt(value) + 1;

	private void Update()
	{
		if (m_forceFinish)
		{
			m_forceFinish = false;
			CheckPages();
		}
		
		if (!m_write) return;
		var changed = false;
		var text = textAsset.Text;
		if (m_skipAction.WasPressedThisFrame())
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

		if (!m_write) ResetWriter();
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
		ResetWriter();
		FullReset();
		m_write = true;
	}

	private void StopWriting()
	{
		ResetWriter();
		FullReset();
		m_write = false;
	}

	private void ResetWriter()
	{
		m_stringBuilder.Clear();
		m_previousPageCount = 0;
		m_timer = 0;
		m_seek = 0;
	}

	private void FullReset()
	{
		textWriter.SetText(m_stringBuilder);
		textWriter.pageToDisplay = 1;
		SetSlider(0);
		slider.SetValueWithoutNotify(0f);
	}
}