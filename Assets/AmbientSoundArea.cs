using System.Collections;
using System.Collections.Generic;
using Attributes;
using Audio;
using Callbacks.Pausing;
using Managers;
using Managers.Persistent;
using Runtime.Automation;
using Runtime.FixedUpdate;
using UnityEngine;
using Utilities;

public class AmbientSoundArea : MonoBehaviour, IFixedUpdatable, IPauseCallback
{
    [SerializeField] [AutoAssigned(AssignMode.Self, typeof(Collider))]
    private Collider m_boxCollider;
    [SerializeField] [AutoAssigned(AssignMode.Child, typeof(AudioSource))]
    private AudioSource m_audioSource;

    [SerializeField] private ClipAggregate ambientClips;

    public FixedUpdatePosition FixedUpdateOrder { get; } = FixedUpdatePosition.Default;
    public void OnFixedUpdate()
    {
        if (!m_audioSource.isPlaying)
        {
            SelectAmbiance();
            m_audioSource.Play();
        }
        m_audioSource.transform.position = m_boxCollider.ClosestPoint(PlayerManager.Instance.PlayerObject.transform.position);
    }

    private void SelectAmbiance()
    {
        var clip = ambientClips.Clips.GetRandom();
        m_audioSource.clip = clip;
        ambientClips.Settings.Apply(m_audioSource);
    }

    private void Start()
    {
        GameManager.Instance.AddFixedUpdateSafe(this);
        PauseManager.Instance.AddPausedCallback(this);

    }
    private void OnDestroy()
    {
        GameManager.Instance?.RemoveFixedUpdate(this);
        PauseManager.Instance?.RemovePausedCallback(this);
    }

    public void OnPauseChanged(bool paused)
    {
        if (paused) m_audioSource.Pause();
        else m_audioSource.UnPause();
    }
}
