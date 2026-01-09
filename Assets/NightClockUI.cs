using System;
using System.Collections;
using System.Collections.Generic;
using Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightClockUI : BaseBehaviour
{
    [SerializeField]
    [Tooltip("The parent GameObject containing the clock hand sprites.")]
    private GameObject clockParent;
    
    [SerializeField]
    private TextMeshProUGUI timerText;

    // References to the transforms of the clock hand sprites.
    [SerializeField] private RectTransform m_shiftStartHand;
    [SerializeField] private RectTransform m_shiftEndHand;
    [SerializeField] private RectTransform m_currentTimeHand;
    [SerializeField] private RectTransform m_secondsHand;
    
    // References to the timer's components used to display and hide it.
    [SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(Image))] 
    private Image m_clockBackground;
    [SerializeField] [AutoAssigned(AssignModeFlags.Self, typeof(RectMask2D))]
    private RectMask2D m_clockMask;
    
    
    public void SetTimerText(string text) => timerText.text = text;
    
    /// <summary>
    /// Updates the rotations of the timer's clock hands.
    /// </summary>
    public void UpdateClock(float timeElapsed, float targetTime)
    {
        var time = TimeSpan.FromSeconds(timeElapsed);
        var secondsRotation = 90 - (time.Seconds) * 6f;
        if (time.Seconds > 30) secondsRotation = -90 + (time.Seconds - 30) * 6f;
        m_secondsHand.rotation = Quaternion.Euler(0, 0, secondsRotation);
        m_currentTimeHand.rotation = Quaternion.Euler(0, 0,
            90.0f - 180.0f * (timeElapsed / targetTime));
    }
    
    /// <summary>
    /// Updates the timer's TextMeshPro countdown.
    /// </summary>
    public void UpdateText(float timeElapsed, float targetTime)
    {
        var time = TimeSpan.FromSeconds(targetTime - timeElapsed);
        //The text is formated to display as 00:00:00.
        timerText.text = $"{time.Hours,2:D2}:{time.Minutes,2:D2}:{time.Seconds,2:D2}";
    }
    
    public void Hide()
    {
        m_clockBackground.enabled = false;
        m_clockMask.softness = new Vector2Int(30000, 30000);
    }
    public void Show()
    {
        m_clockBackground.enabled = true;
        m_clockMask.softness = new Vector2Int(0, 0);
    }
}
