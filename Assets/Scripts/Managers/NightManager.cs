using System;
using System.Collections.Generic;
using System.ComponentModel;
using Attributes;
using AYellowpaper.SerializedCollections;
using Interactables;
using Night;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;

namespace Managers
{
    [AddComponentMenu("Managers/Night Manager")]
    public sealed class NightManager : Singleton<NightManager>, IFrameUpdatable
    {
        private struct TimedInteractable
        {
            public NightTime TimeToPass;
            public Interactable Interactable;

            public TimedInteractable(NightTime timeToPass, Interactable interactable)
            {
                TimeToPass = timeToPass;
                Interactable = interactable;
            }
        }
        
        [Category("Time")] 
        [SerializeField] private NightTime startingTime;
        [SerializeField] private NightTime endingTime;

        [SerializeField]
        [Tooltip("The amount of seconds added when a player interacts with an interactable for the first time.")]
        private float interactionTimeJump = 30 * 60;

        [SerializeField]
        [Tooltip("How much the time jump is reduced on repeated interaction with the same interactable.")]
        [Range(0.0f, 1.0f)]
        private float repeatReduction = 0.1f;
        
        [Category("Night actions")] 
        [CanBeNullInPrefab, SerializeField] private Interactable nightEndActions;
        [CanBeNullInPrefab, SerializeField] private SerializedDictionary<NightTime, Interactable> scheduledActions;
        
        private Queue<TimedInteractable> m_scheduledActions = new();
        private Dictionary<string, int> m_interactionLog = new();
        private NightTime m_currentTime;
        private State m_state;
        private float m_timer;
        
        private enum State : byte
        {
            Counting,
            WantsToFinish,
            Finished,
        }

        public event Action<NightTime> OnTimeChanged;
        
        protected override bool Override => true;

        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.NightManager;

        public void OnFrameUpdate()
        {
            switch (m_state)
            {
                case State.Counting:
                    break;
                case State.WantsToFinish:
                    // if CanFinish
                    m_state = State.Finished;
                    nightEndActions.Interact();
                    break;
                case State.Finished:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            m_timer += Time.deltaTime;
            TimerChanged();
        }

        private void TimerChanged()
        {
            if (m_currentTime.Update(ref m_timer))
            {
                while (m_scheduledActions.Count > 0)
                {
                    var timedInteractable = m_scheduledActions.Peek();
                    if (!timedInteractable.TimeToPass.Passed(m_currentTime)) break;
                    timedInteractable = m_scheduledActions.Dequeue();
                    timedInteractable.Interactable.Interact();
                }
                
                OnTimeChanged?.Invoke(m_currentTime);
                if (m_state == State.Counting && m_currentTime.Passed(endingTime)) m_state = State.WantsToFinish;
            }
        }
        
        /// <summary>
        /// Fast-Forward the night timer by <paramref name="time"/> seconds.
        /// </summary>
        /// <param name="time"></param>
        private void ForwardTime(float time)
        {
            m_timer += time;
            TimerChanged();
        }

        /// <summary>
        /// Register the player interaction with an Interactable.
        /// </summary>
        public void RegisterInteraction(string interactionID, int timeToAdd = 0)
        {
            var reduction = 1.0f;
            if (!m_interactionLog.TryAdd(interactionID, 1))
                reduction = repeatReduction / ++m_interactionLog[interactionID];
            if (timeToAdd == 0) ForwardTime(reduction * interactionTimeJump);
            else ForwardTime(reduction * timeToAdd);
        }
        
        private void Start()
        {
            m_currentTime = startingTime;
            var list = new List<TimedInteractable>();
            foreach (var pair in scheduledActions) list.Add(new TimedInteractable(pair.Key, pair.Value));
            list.Sort(CompareTimes);
            for (var i = 0; i < list.Count; i++) Debug.Log(list[i].TimeToPass);
        }

        private static int CompareTimes(TimedInteractable x, TimedInteractable y)
        {
            var xTime = x.TimeToPass;
            var yTime = y.TimeToPass;
            var result = xTime.hour.CompareTo(yTime.hour);
            if (result != 0) return result;
            result = xTime.minute.CompareTo(yTime.minute);
            if (result != 0) return result;
            result = xTime.second.CompareTo(yTime.second);
            return result;
        }
#if UNITY_EDITOR
        public NightTime CurrentTime => m_currentTime;
#endif
    }
}