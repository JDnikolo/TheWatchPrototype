using System.Collections.Generic;
using System.Linq;
using Audio;
using Interactables.Triggers;
using Managers;
using Managers.Persistent;
using Runtime.FrameUpdate;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utilities;

public class ShoutCaster : MonoBehaviour, IFrameUpdatable
{
    private const int MaxInteractables = 10;
    
    [FormerlySerializedAs("playerTransform")] [SerializeField] private Transform playerCameraTransform;
    
    [Header("Animation")]
    
    [SerializeField] private ParticleSystem shoutParticles;
    
    [Header("Audio")]
    [SerializeField] private AudioSource shoutSource;

    [SerializeField] private ClipAggregate shoutClips;

    [SerializeField] private string shoutActionName;

    private InputAction m_shoutAction;
    
    [Header("Parameters")]
    [SerializeField] private float shoutInteractDistance;
    
    [SerializeField] private float shoutWidth;
    
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private LayerMask terrainLayers;
    
    private int m_layerMask;
    private int m_terrainMask;
    private void Awake()
    {
        m_layerMask = interactableLayers.value;
        m_terrainMask = terrainLayers.value;
        GameManager.Instance?.AddFrameUpdate(this);
    }

    private void CastShout()
    {
        if (shoutSource.isPlaying) return;
        
        PlayShoutAudio();
        PlayShoutAnimation();
        
        Collider[] interactables = new Collider[MaxInteractables];

        var center = playerCameraTransform.position +
                     playerCameraTransform.forward * shoutInteractDistance / 2;
        var halfExtents  = new Vector3(shoutWidth/2, shoutWidth/2, shoutInteractDistance/2);
        
        var interactableCount = UnityEngine.Physics.OverlapBoxNonAlloc(
            center, halfExtents, interactables, 
            playerCameraTransform.rotation, m_layerMask, QueryTriggerInteraction.Ignore
            );
        
        if (interactableCount == 0) return;
        
        var unblockedInteractables = new SortedList<float, Collider>();
        for (var i = 0; i < interactableCount; i++)
        {
            var distance = Vector3.Distance(playerCameraTransform.position, 
                interactables[i].transform.position);
            if (!UnityEngine.Physics.Raycast(playerCameraTransform.position,
                    interactables[i].transform.position - playerCameraTransform.position, 
                    distance, m_terrainMask, QueryTriggerInteraction.Ignore))
            {
                unblockedInteractables.Add(distance, interactables[i]);
            }
        }
        if (unblockedInteractables.Count == 0) return;
        
       var shoutTrigger = unblockedInteractables.First().Value.GetComponent<InteractableShoutTrigger>();
        
       shoutTrigger?.OnGettingShouted();
    }

    private void PlayShoutAudio()
    {
        var clip = shoutClips.Clips.GetRandom();
        shoutSource.clip = clip;
        shoutClips.Settings.Apply(shoutSource);
        shoutSource.Play();
    }

    private void PlayShoutAnimation()
    {
        shoutParticles.transform.rotation = playerCameraTransform.rotation;
        shoutParticles.Play();
        CinematicManager.Instance.ShakeCameraFOV(
            CameraManager.Instance.Camera.gameObject.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera,
            0.5f
            );
    }

    public FrameUpdatePosition FrameUpdateOrder { get; } = FrameUpdatePosition.Player;
    public void OnFrameUpdate()
    {
        m_shoutAction ??= InputManager.Instance.PlayerMap.GetAction(shoutActionName);
        if (m_shoutAction.WasPressedThisFrame()) CastShout();
    }

    public void OnDestroy()
    {
        GameManager.Instance?.RemoveFrameUpdate(this);
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        var rotationMatrix = Matrix4x4.TRS(playerCameraTransform.position, playerCameraTransform.rotation, playerCameraTransform.lossyScale);
        Gizmos.matrix = rotationMatrix;	
        var center = Vector3.forward * shoutInteractDistance / 2;
        var size  = Vector3.right * shoutWidth + Vector3.up * shoutWidth
                                               + Vector3.forward * shoutInteractDistance;
        Gizmos.DrawWireCube(center, size);
    }

#endif

}
