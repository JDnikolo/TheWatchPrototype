using System.ComponentModel;
using UI;
using UnityEngine;

namespace Managers
{
    public class NightManager : Singleton<NightManager>
    {
        private NightTimerUI m_timer;

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

        private void Start()
        {
            m_timer = GameObject.FindGameObjectWithTag("NightTimer").GetComponent<NightTimerUI>();
            m_timer.SetTargetTime(nightTime);
            m_timer.TimerFinished += OnTimerFinished;
        }


        /// <summary>
        /// Fast-Forward the night timer by <paramref name="time"/> seconds.
        /// </summary>
        /// <param name="time"></param>
        public void ForwardTime(float time) => m_timer.FastForwardSeconds(time);

        /// <summary>
        /// Register the player interaction with an Interactable.
        /// </summary>
        /// <param name="timesInteracted">The amount of times the player has alread interacted with the Interactable.</param>
        public void RegisterInteraction(int timesInteracted)
        {
            var reduction = (timesInteracted == 0) ? 1 : repeatReduction / timesInteracted;
            ForwardTime(reduction * interactionTimeJump);
        }

        private void OnTimerFinished()
        {
            //TODO: Finish the night section
        }
    }
}
