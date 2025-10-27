using System;
using System.Collections.Generic;
using System.ComponentModel;
using Callbacks;
using UI;
using UnityEngine;

namespace Managers
{
    [AddComponentMenu("Managers/Night Manager")]
    public sealed class NightManager : Singleton<NightManager>
    {
        [SerializeField] private NightTimerUI m_timer;

        [Category("Time")]
        [SerializeField]
        [Tooltip("The total duration of the night section in seconds.")]
        private float nightTime = 7200.0f;

        [SerializeField]
        [Tooltip("The amount of seconds added when a player interacts with an interactable for the first time.")]
        private float interactionTimeJump = 30 * 60;
        [SerializeField]
        [Tooltip("How much the time jump is reduced on repeated interaction with the same interactable.")]
        [Range(0.0f, 1.0f)]
        private float repeatReduction = 0.1f;
        [SerializeReference] private NightEndActions m_nightEndActions;
        
        private Dictionary<string, int> m_interactionLog = new Dictionary<string, int>();
    

        private void Start()
        {
            m_timer.SetTargetTime(nightTime);
            m_timer.TimerFinished += OnTimerFinished;
        }
        
        /// <summary>
        /// Fast-Forward the night timer by <paramref name="time"/> seconds.
        /// </summary>
        /// <param name="time"></param>
        private void ForwardTime(float time) => m_timer.FastForwardSeconds(time);

        /// <summary>
        /// Register the player interaction with an Interactable.
        /// </summary>
        public void RegisterInteraction(string interactionID, int timeToAdd = 0)
        {
            var reduction = 1.0f;
            
            if (!m_interactionLog.TryAdd(interactionID, 1))
            {
                reduction = repeatReduction / ++m_interactionLog[interactionID];
            }
            if (timeToAdd == 0) ForwardTime(reduction * interactionTimeJump);
            else ForwardTime(reduction * timeToAdd);
            ShowTimer();
        }

        public void ResetInteractionLog() => m_interactionLog.Clear();

        private void OnTimerFinished()
        {
            m_nightEndActions?.DoActions();
            //TODO: Finish the night section
        }

        public void ShowTimer() => m_timer.Show();
    }
}
