using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NightManager : MonoBehaviour
{
    private static NightManager m_instance;

    public static NightManager Instance { get => m_instance; }

    private GameObject m_Player;
    private NightTimerUI m_timer;


    [SerializeField]
    [Tooltip("The amount of seconds added when a player interacts with an interactable for the first time.")]
    private float interactionTimeJump = 30 * 60;
    [SerializeField]
    [Tooltip("How much the time jump is reduced on repeated interaction with an interactable.")]
    [Range(0.0f, 1.0f)]
    private float repeatReduction = 0.1f;

    private void Awake()
    {
        if (m_instance != null && m_instance != this) Destroy(this.gameObject);
        else m_instance = this;
    }

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player"); //TODO: Replace with getting a reference from a PlayerManager when that is implemented.
        m_timer = GameObject.FindGameObjectWithTag("NightTimer").GetComponent<NightTimerUI>();
    }

    private void ForwardTime(float time)
    {
        m_timer.FastForwardSeconds(time);
    }

    public void RegisterInteraction(int timesInteracted)
    {
        var reduction = (timesInteracted == 0) ? 1 : repeatReduction / timesInteracted;
        ForwardTime(reduction * interactionTimeJump);
    }
}
