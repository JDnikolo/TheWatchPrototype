using System.Collections.Generic;
using System.ComponentModel;
using Callbacks.Night;
using Runtime;
using UI.Night;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    [AddComponentMenu("Managers/Night Manager")]
    public sealed class NightManager : Singleton<NightManager>
    {
        [FormerlySerializedAs("m_timer")] [SerializeField] private NightTimer timer;

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
        [FormerlySerializedAs("m_nightEndActions")] [SerializeReference] private NightEndActions nightEndActions;

        private Dictionary<string, int> m_interactionLog = new Dictionary<string, int>();

        protected override bool Override => true;

        private void Start()
        {
            timer.SetTargetTime(nightTime);
            timer.TimerFinished += OnTimerFinished;
        }
        
        /// <summary>
        /// Fast-Forward the night timer by <paramref name="time"/> seconds.
        /// </summary>
        /// <param name="time"></param>
        private void ForwardTime(float time) => timer.FastForwardSeconds(time);

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
            nightEndActions?.DoActions();
        }

        public void ShowTimer() => timer.Show();
    }
}
