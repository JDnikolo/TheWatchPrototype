using System;
using System.Globalization;
using Attributes;
using Runtime;
using Runtime.FrameUpdate;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Night
{
    [AddComponentMenu("UI/Night/Night Timer")]
    public class NightTimer : BaseBehaviour, IFrameUpdatable
    {
        /// <summary>
        /// Event invoked when the timer's target time is set.
        /// </summary>
        public event Action<float> TargetTimeSet;

        /// <summary>
        /// Event invoked when the timer reaches its target time.
        /// </summary>
        public event Action TimerFinished;
        
        private float targetTime = 2 * 60 * 60.0f;

        /// <summary>
        /// The time the timer has counted up so far.
        /// </summary>
        private float m_timeElapsed;

        /// <summary>
        /// Secondary timer used to update the timer UI once per second.
        /// </summary>
        private float m_secondTimer = 0.0f;

        /// <summary>
        /// Boolean 
        /// </summary>
        private Updatable m_updatable;
        
        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.GameUI;
        
        [FormerlySerializedAs("m_nightClock")] [SerializeField] private NightClockUI nightClockUI;
        [SerializeField] [CanBeNull] private NightActionScheduler nightActionScheduler;
        
        private void Awake()
        {
            m_timeElapsed = 0.0f;
            m_updatable.SetUpdating(true, this);
        }

        private void Start()
        {
            nightClockUI.UpdateText(m_timeElapsed, targetTime);
            nightClockUI.UpdateClock(m_timeElapsed, targetTime);
        }

        public void OnFrameUpdate()
        {
            if (m_timeElapsed <= targetTime) m_timeElapsed += Time.deltaTime;
            else
            {
                nightClockUI.SetTimerText("Shift Over!");
                TimerFinished?.Invoke();
                m_updatable.SetUpdating(false, this);
            }

            //Only update display once a second
            m_secondTimer -= Time.deltaTime;
            if (m_secondTimer <= 0)
            {
                m_secondTimer += 1.0f;
                nightClockUI.UpdateText(m_timeElapsed, targetTime);
                nightClockUI.UpdateClock(m_timeElapsed, targetTime);
                nightActionScheduler?.CheckForScheduledActions(m_timeElapsed);
            }
        }

        /// <summary>
        /// Hides the timer.
        /// </summary>
        public void Hide()
        {
            nightClockUI.Hide();
        }
        /// <summary>
        /// Makes the timer visible.
        /// </summary>
        public void Show()
        {
            nightClockUI.Show();
        }

        /// <summary>
        /// Enables or disables the timer. Enables the timer by default.
        /// </summary>
        /// <param name="value">Set to true to enable, false to disable.</param>
        public void SetEnabled(bool value = true) => m_updatable.SetUpdating(value, this);
        

        

        /// <summary>
        /// Sets the timer's target time and resets the timer's graphical components
        /// </summary>
        /// <param name="newvalue">The new target time to count towards.</param>
        public void SetTargetTime(float newvalue)
        {
            targetTime = newvalue;
            TargetTimeSet?.Invoke(newvalue);

            m_timeElapsed = 0.0f;
            m_secondTimer = 0.0f;

            nightClockUI.UpdateText(m_timeElapsed, targetTime);
            nightClockUI.UpdateClock(m_timeElapsed, targetTime);
        }

        /// <summary>
        /// Increases the elapsed time by the given amount of seconds.
        /// </summary>
        /// <param name="seconds">The amount of seconds to forward the timer by.</param>
        public void FastForwardSeconds(float seconds)
        {
            m_timeElapsed += Mathf.Clamp(seconds, 0, targetTime - m_timeElapsed);
            nightClockUI.UpdateText(m_timeElapsed, targetTime);
            nightClockUI.UpdateClock(m_timeElapsed, targetTime);
        }

        /// <summary>
        /// Increases the time elapsed by a percentage of the target time.
        /// </summary>
        /// <param name="percent"></param>
        public void FastForwardProgress(float percent)
        {
            m_timeElapsed += Mathf.Clamp(targetTime * percent, 0, targetTime - m_timeElapsed);

            nightClockUI.UpdateText(m_timeElapsed, targetTime);
            nightClockUI.UpdateClock(m_timeElapsed, targetTime);
        }

        /// <summary>
        /// Sets the timer's elapsed time to the specified amount of seconds.
        /// </summary>
        /// <param name="seconds"></param>
        public void SetTimeElapsedSeconds(float seconds)
        {
            m_timeElapsed = Mathf.Clamp(seconds, 0, Mathf.Infinity);

            nightClockUI.UpdateText(m_timeElapsed, targetTime);
            nightClockUI.UpdateClock(m_timeElapsed, targetTime);
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

            nightClockUI.UpdateText(m_timeElapsed, targetTime);
            nightClockUI.UpdateClock(m_timeElapsed, targetTime);
        }
    }
}