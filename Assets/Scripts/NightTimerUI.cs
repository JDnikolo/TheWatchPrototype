using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using System.Globalization;
using UnityEngine.Serialization;

public class NightTimerUI : MonoBehaviour
{
    /// <summary>
    /// Event invoked when the timer's target time is set.
    /// </summary>
    [FormerlySerializedAs("TargetTimeSet")] [Tooltip("Invoked when the timer's target time is set.")] [HideInInspector]
    public UnityEvent<float> targetTimeSet;

    /// <summary>
    /// Event invoked when the timer reaches its target time.
    /// </summary>
    [FormerlySerializedAs("TimerFinished")]
    [Tooltip("Invoked when the timer reaches its target time.")]
    [HideInInspector]
    //For some reason this keeps throwing errors in the inspector?
    public UnityEvent timerFinished;

    [SerializeField] [Tooltip("The parent GameObject containing the clock hand sprites.")]
    private GameObject clockParent;

    [SerializeField] [Tooltip("The time that the counter will count towards.")]
    private float targetTime = 2 * 60 * 60.0f;

    /// <summary>
    /// The time the timer has counted up so far.
    /// </summary>
    private float m_timeElapsed;

    /// <summary>
    /// Secondary timer used to update the timer UI once per second.
    /// </summary>
    private float m_secondTimer = 1.0f;

    /// <summary>
    /// Boolean 
    /// </summary>
    private bool m_isCounting = true;

    private TextMeshProUGUI m_timerText;

    // References to the transforms of the clock hand sprites.
    private RectTransform m_shiftStartHand;
    private RectTransform m_shiftEndHand;
    private RectTransform m_currentTimeHand;
    private RectTransform m_secondsHand;
    
    private void Awake()
    {
        m_timerText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        var clockHands = clockParent.GetComponentsInChildren<RectTransform>();
        m_shiftStartHand = clockHands[1];
        m_shiftEndHand = clockHands[2];
        m_currentTimeHand = clockHands[3];
        m_secondsHand = clockHands[4];
        m_timeElapsed = 0.0f;
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }

    private void Update()
    {
        if (!m_isCounting) return;
        if (m_timeElapsed <= targetTime) m_timeElapsed += Time.deltaTime;
        else
        {
            timerFinished.Invoke();
            m_isCounting = false;
        }

        //Only update display once a second
        m_secondTimer -= Time.deltaTime;
        if (m_secondTimer <= 0)
        {
            m_secondTimer += 1.0f;
            UpdateText();
            UpdateClock();
        }
    }

    /// <summary>
    /// Enables or disables the timer. Enables the timer by default.
    /// </summary>
    /// <param name="value">Set to true to enable, false to disable.</param>
    public void SetEnabled(bool value = true) => m_isCounting = value;

    /// <summary>
    /// Sets the positions of the shift end clock hand according to the timer's target time.
    /// </summary>
    public void SetupClockHands()
    {
        var time = TimeSpan.FromSeconds(targetTime);
        m_shiftEndHand.rotation = Quaternion.Euler(0, 0,
            (time.Hours) * -30.0f + (time.Minutes % 30) * -15.0f);
    }

    /// <summary>
    /// Updates the rotations of the timer's clock hands.
    /// </summary>
    private void UpdateClock()
    {
        var time = TimeSpan.FromSeconds(m_timeElapsed);
        m_secondsHand.rotation = Quaternion.Euler(0, 0, (time.Seconds) * -6.0f);
        var endangle = (TimeSpan.FromSeconds(targetTime).Hours) * -30.0f +
                       (TimeSpan.FromSeconds(targetTime).Minutes % 30) * -15.0f;
        m_currentTimeHand.rotation = Quaternion.Euler(0, 0, endangle * (m_timeElapsed / targetTime));
    }

    /// <summary>
    /// Updates the timer's TextMeshPro countdown.
    /// </summary>
    private void UpdateText()
    {
        var time = TimeSpan.FromSeconds(targetTime - m_timeElapsed);
        //The text is formated to display as 00:00:00.
        m_timerText.text = $"{time.Hours,2:D2}:{time.Minutes,2:D2}:{time.Seconds,2:D2}";
    }

    /// <summary>
    /// Sets the timer's target time and resets the timer's graphical components
    /// </summary>
    /// <param name="newvalue">The new target time to count towards.</param>
    public void SetTargetTime(float newvalue)
    {
        targetTime = newvalue;
        targetTimeSet.Invoke(newvalue);

        m_timeElapsed = 0.0f;
        m_secondTimer = 0.0f;

        SetupClockHands();
        UpdateText();
        UpdateClock();
    }

    /// <summary>
    /// Increases the elapsed time by the given amount of seconds.
    /// </summary>
    /// <param name="seconds">The amount of seconds to forward the timer by.</param>
    public void FastForwardSeconds(float seconds)
    {
        m_timeElapsed += Mathf.Clamp(seconds, 0, targetTime - m_timeElapsed);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }

    /// <summary>
    /// Increases the time elapsed by a percentage of the target time.
    /// </summary>
    /// <param name="percent"></param>
    public void FastForwardProgress(float percent)
    {
        m_timeElapsed += Mathf.Clamp(targetTime * percent, 0, targetTime - m_timeElapsed);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }

    /// <summary>
    /// Sets the timer's elapsed time to the specified amount of seconds.
    /// </summary>
    /// <param name="seconds"></param>
    public void SetTimeElapsedSeconds(float seconds)
    {
        m_timeElapsed = Mathf.Clamp(seconds, 0, Mathf.Infinity);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }

    /// <summary>
    /// Parses the given string to set the timer's elapsed time to the specified amount of seconds.
    /// </summary>
    /// <param name="secondsstring"></param>
    public void SetCurrentCount(string secondsstring) => 
        SetTimeElapsedSeconds(float.Parse(secondsstring, CultureInfo.InvariantCulture));

    /// <summary>
    /// Sets the timer's time elapsed to a percent of its target time.
    /// </summary>
    /// <param name="percent"></param>
    public void SetProgress(float percent)
    {
        m_timeElapsed = Mathf.Clamp(percent * targetTime, 0, targetTime);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }
}