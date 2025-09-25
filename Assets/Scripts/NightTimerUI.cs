using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using System.Globalization;
using Unity.VisualScripting;

public class NightTimerUI : MonoBehaviour
{
    /// <summary>
    /// Event invoked when the timer's target time is set.
    /// </summary>
    [Tooltip("Invoked when the timer's target time is set.")]
    [HideInInspector]
    public UnityEvent<float> TargetTimeSet;


    /// <summary>
    /// Event invoked when the timer reaches its target time.
    /// </summary>
    [Tooltip("Invoked when the timer reaches its target time.")]
    [HideInInspector] //For some reason this keeps throwing errors in the inspector?
    public UnityEvent TimerFinished;

    [SerializeField]
    [Tooltip("The parent GameObject containing the clock hand sprites.")]
    private GameObject clockParent;

    [SerializeField]
    [Tooltip("The time that the counter will count towards.")]
    private float targetTime = 2 * 60 * 60.0f;

    /// <summary>
    /// The time the timer has counted up so far.
    /// </summary>
    private float timeElapsed = 0.0f;

    /// <summary>
    /// Secondary timer used to update the timer UI once per second.
    /// </summary>
    private float secondTimer = 1.0f;
    /// <summary>
    /// Boolean 
    /// </summary>
    private bool isCounting = true;

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
        timeElapsed = 0.0f;
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }

    private void Update()
    {
        if (!isCounting) return;

        if (timeElapsed <= targetTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            TimerFinished.Invoke();
            isCounting = false;
        }
        //Only update display once a second
        secondTimer -= Time.deltaTime;
        if (secondTimer <= 0)
        {
            secondTimer += 1.0f;
            UpdateText();
            UpdateClock();
        }
    }

    /// <summary>
    /// Enables or disables the timer. Enables the timer by default.
    /// </summary>
    /// <param name="value">Set to true to enable, false to disable.</param>
    public void SetEnabled(bool value = true)
    {
        isCounting = value;
    }
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
        var time = TimeSpan.FromSeconds(timeElapsed);
        m_secondsHand.rotation = Quaternion.Euler(0, 0, (time.Seconds) * -6.0f);
        var endangle = (TimeSpan.FromSeconds(targetTime).Hours) * -30.0f + (TimeSpan.FromSeconds(targetTime).Minutes % 30) * -15.0f;
        m_currentTimeHand.rotation = Quaternion.Euler(0, 0, endangle * (timeElapsed / targetTime));
    }
    /// <summary>
    /// Updates the timer's TextMeshPro countdown.
    /// </summary>
    private void UpdateText()
    {
        var time = TimeSpan.FromSeconds(targetTime - timeElapsed);
        //The text is formated to display as 00:00:00.
        m_timerText.text = string.Format("{0,2:D2}:{1,2:D2}:{2,2:D2}",
            time.Hours,
            time.Minutes,
            time.Seconds);
    }
    /// <summary>
    /// Sets the timer's target time and resets the timer's graphical components
    /// </summary>
    /// <param name="newvalue">The new target time to count towards.</param>
    public void SetTargetTime(float newvalue)
    {
        targetTime = newvalue;
        TargetTimeSet.Invoke(newvalue);

        timeElapsed = 0.0f;
        secondTimer = 0.0f;

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
        timeElapsed += Mathf.Clamp(seconds, 0, targetTime - timeElapsed);
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
        timeElapsed += Mathf.Clamp(targetTime * percent, 0, targetTime - timeElapsed);
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
        timeElapsed = Mathf.Clamp(seconds, 0, Mathf.Infinity);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }
    /// <summary>
    /// Parses the given string to set the timer's elapsed time to the specified amount of seconds.
    /// </summary>
    /// <param name="secondsstring"></param>
    public void SetCurrentCount(string secondsstring)
    {
        SetTimeElapsedSeconds(float.Parse(secondsstring, CultureInfo.InvariantCulture));
    }
    /// <summary>
    /// Sets the timer's time elapsed to a percent of its target time.
    /// </summary>
    /// <param name="percent"></param>
    public void SetProgress(float percent)
    {
        timeElapsed = Mathf.Clamp(percent * targetTime, 0, targetTime);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }
}
