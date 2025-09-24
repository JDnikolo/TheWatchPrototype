using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using System.Globalization;
using Unity.VisualScripting;

public class NightTimerUI : MonoBehaviour
{

    private TextMeshProUGUI _timerText;
    // References to the transforms of the clock hand sprites.
    private RectTransform _shiftStartHand;
    private RectTransform _shiftEndHand;
    private RectTransform _currentTimeHand;
    private RectTransform _secondsHand;

    [SerializeField]
    [Tooltip("The parent GameObject containing the clock hand sprites.")]
    private GameObject _clockParent;



    [SerializeField]
    [Tooltip("The time that the counter will count towards.")]
    private float _targetTime = 2 * 60 * 60.0f;

    /// <summary>
    /// Event invoked when the timer's target time is set.
    /// </summary>
    [Tooltip("Invoked when the timer's target time is set.")]
    [HideInInspector]
    public UnityEvent<float> OnTargetTimeSet;


    /// <summary>
    /// Event invoked when the timer reaches its target time.
    /// </summary>
    [Tooltip("Invoked when the timer reaches its target time.")]
    [HideInInspector] //For some reason this keeps throwing errors in the inspector?
    public UnityEvent OnTimerFinished;

    /// <summary>
    /// The time the timer has counted up so far.
    /// </summary>
    private float _timeElapsed = 0.0f;

    /// <summary>
    /// Secondary timer used to update the timer UI once per second.
    /// </summary>
    private float _secondTimer = 1.0f;
    /// <summary>
    /// Boolean 
    /// </summary>
    private bool _isCounting = true;

    void Awake()
    {
        _timerText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        var clock_hands = _clockParent.GetComponentsInChildren<RectTransform>();
        _shiftStartHand = clock_hands[1];
        _shiftEndHand = clock_hands[2];
        _currentTimeHand = clock_hands[3];
        _secondsHand = clock_hands[4];
        _timeElapsed = 0.0f;
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }

    void Update()
    {
        if (!_isCounting) return;

        if (_timeElapsed <= _targetTime)
        {
            _timeElapsed += Time.deltaTime;
        }
        else
        {
            OnTimerFinished.Invoke();
            _isCounting = false;
        }
        //Only update display once a second
        _secondTimer -= Time.deltaTime;
        if (_secondTimer <= 0)
        {
            _secondTimer += 1.0f;
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
        _isCounting = value;
    }
    /// <summary>
    /// Sets the positions of the shift end clock hand according to the timer's target time.
    /// </summary>
    public void SetupClockHands()
    {
        var time = TimeSpan.FromSeconds(_targetTime);
        _shiftEndHand.rotation = Quaternion.Euler(0, 0,
            (time.Hours) * -30.0f + (time.Minutes % 30) * -15.0f);
    }
    /// <summary>
    /// Updates the rotations of the timer's clock hands.
    /// </summary>
    private void UpdateClock()
    {
        var time = TimeSpan.FromSeconds(_timeElapsed);
        _secondsHand.rotation = Quaternion.Euler(0, 0, (time.Seconds) * -6.0f);
        var end_angle = (TimeSpan.FromSeconds(_targetTime).Hours) * -30.0f + (TimeSpan.FromSeconds(_targetTime).Minutes % 30) * -15.0f;
        _currentTimeHand.rotation = Quaternion.Euler(0, 0, end_angle * (_timeElapsed / _targetTime));
    }
    /// <summary>
    /// Updates the timer's TextMeshPro countdown.
    /// </summary>
    private void UpdateText()
    {
        var time = TimeSpan.FromSeconds(_targetTime - _timeElapsed);
        //The text is formated to display as 00:00:00.
        _timerText.text = string.Format("{0,2:D2}:{1,2:D2}:{2,2:D2}",
            time.Hours,
            time.Minutes,
            time.Seconds);
    }
    /// <summary>
    /// Sets the timer's target time and resets the timer's graphical components
    /// </summary>
    /// <param name="new_value">The new target time to count towards.</param>
    public void SetTargetTime(float new_value)
    {
        _targetTime = new_value;
        OnTargetTimeSet.Invoke(new_value);

        _timeElapsed = 0.0f;
        _secondTimer = 0.0f;

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
        _timeElapsed += Mathf.Clamp(seconds, 0, _targetTime - _timeElapsed);
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
        _timeElapsed += Mathf.Clamp(_targetTime * percent, 0, _targetTime - _timeElapsed);
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
        _timeElapsed = Mathf.Clamp(seconds, 0, Mathf.Infinity);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }
    /// <summary>
    /// Parses the given string to set the timer's elapsed time to the specified amount of seconds.
    /// </summary>
    /// <param name="seconds_string"></param>
    public void SetCurrentCount(string seconds_string)
    {
        SetTimeElapsedSeconds(float.Parse(seconds_string, CultureInfo.InvariantCulture));
    }
    /// <summary>
    /// Sets the timer's time elapsed to a percent of its target time.
    /// </summary>
    /// <param name="percent"></param>
    public void SetProgress(float percent)
    {
        _timeElapsed = Mathf.Clamp(percent * _targetTime, 0, _targetTime);
        SetupClockHands();
        UpdateText();
        UpdateClock();
    }
}
