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
    
    [FormerlySerializedAs("shoutInteractDistance")]
    [Header("Parameters")]
    [SerializeField] private float shoutInteractDistanceOuter;
    
    [FormerlySerializedAs("shoutWidth")] [SerializeField] private float shoutWidthOuter;
    
    [SerializeField] private float shoutInteractDistanceInner;
    
    [SerializeField] private float shoutWidthInner;
    
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
        
        Collider[] interactablesInner = new Collider[MaxInteractables];
        
        var centerInner = playerCameraTransform.position +
                          playerCameraTransform.forward * shoutInteractDistanceInner / 2;
        var halfExtentsInner  = new Vector3(shoutWidthInner/2, shoutWidthInner/2, shoutInteractDistanceInner/2);
        
        var interactableCountInner = UnityEngine.Physics.OverlapBoxNonAlloc(
            centerInner, halfExtentsInner, interactablesInner, 
            playerCameraTransform.rotation, m_layerMask, QueryTriggerInteraction.Ignore
        );
        
        Collider[] interactablesOuter = new Collider[MaxInteractables];

        var centerOuter = playerCameraTransform.position +
                     playerCameraTransform.forward * (shoutInteractDistanceOuter / 2 + shoutInteractDistanceInner);
        var halfExtentsOuter  = new Vector3(shoutWidthOuter/2, shoutWidthOuter/2, shoutInteractDistanceOuter/2);
        
        var interactableCountOuter = UnityEngine.Physics.OverlapBoxNonAlloc(
            centerOuter, halfExtentsOuter, interactablesOuter, 
            playerCameraTransform.rotation, m_layerMask, QueryTriggerInteraction.Ignore
            );
        
        if (interactableCountOuter == 0 && interactableCountInner == 0) return;

        var interactables = new HashSet<Collider>();
        for (var i = 0; i < interactableCountInner; i++) interactables.Add(interactablesInner[i]);
        for (var i = 0; i < interactableCountOuter; i++) interactables.Add(interactablesOuter[i]);
        
        var unblockedInteractables = new SortedList<float, Collider>();
        foreach (var interactable in interactables)
        {
            var distance = Vector3.Distance(playerCameraTransform.position, 
                interactable.transform.position);
            if (!UnityEngine.Physics.Raycast(playerCameraTransform.position,
                    interactable.transform.position - playerCameraTransform.position, 
                    distance, m_terrainMask, QueryTriggerInteraction.Ignore))
            {
                unblockedInteractables.Add(distance, interactable);
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
        var center = Vector3.forward * shoutInteractDistanceInner / 2;
        var size  = Vector3.right * shoutWidthInner + Vector3.up * shoutWidthInner
                                               + Vector3.forward * shoutInteractDistanceInner;
        Gizmos.DrawWireCube(center, size);
        center = Vector3.forward * (shoutInteractDistanceOuter / 2 + shoutInteractDistanceInner);
        size  = Vector3.right * shoutWidthOuter + Vector3.up * shoutWidthOuter
                                                    + Vector3.forward * shoutInteractDistanceOuter;
        Gizmos.DrawWireCube(center, size);
    }

#endif

}
