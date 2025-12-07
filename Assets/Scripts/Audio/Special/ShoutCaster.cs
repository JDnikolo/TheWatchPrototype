using System.Collections.Generic;
using System.Linq;
using Interactables.Triggers;
using LookupTables;
using Managers;
using Managers.Persistent;
using Runtime.FrameUpdate;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audio.Special
{
    public sealed class ShoutCaster : BaseBehaviour, IFrameUpdatable
    {
        [SerializeField] private ParticleSystem shoutParticles;
        [SerializeField] private AudioPlayer shoutPlayer;
        [SerializeField] private AudioAggregate shoutAudios;
        [SerializeField] private string shoutActionName;

        [Header("Parameters")] 
        // ReSharper disable once MissingLinebreak
        [SerializeField] private float shoutInteractDistanceOuter;
        [SerializeField] private float shoutWidthOuter;
        [SerializeField] private float shoutInteractDistanceInner;
        [SerializeField] private float shoutWidthInner;
        [SerializeField] private LayerMask interactableLayers;
        [SerializeField] private LayerMask terrainLayers;

        private const int MaxInteractables = 10;
        private SortedList<float, InteractableShoutTrigger> m_unblockedInteractables = new();
        private HashSet<InteractableShoutTrigger> m_singleInteractables = new();
        private Collider[] m_interactablesInner = new Collider[MaxInteractables];
        private Collider[] m_interactablesOuter = new Collider[MaxInteractables];
        private InputAction m_shoutAction;

        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Player;

        public void OnFrameUpdate()
        {
            m_shoutAction ??= InputManager.Instance.PlayerMap.GetAction(shoutActionName);
            if (m_shoutAction.WasPressedThisFrame() && !shoutPlayer.IsPlaying)
            {
                PlayShoutAudio();
                PlayShoutAnimation();
                var cameraTransform = PlayerManager.Instance.PlayerCamera.transform;
                var innerLength = UnityEngine.Physics.OverlapBoxNonAlloc(
                    cameraTransform.position + cameraTransform.forward * shoutInteractDistanceInner / 2,
                    new Vector3(shoutWidthInner / 2, shoutWidthInner / 2, shoutInteractDistanceInner / 2),
                    m_interactablesInner, cameraTransform.rotation, interactableLayers, QueryTriggerInteraction.Ignore
                );

                var outerLength = UnityEngine.Physics.OverlapBoxNonAlloc(
                    cameraTransform.position + cameraTransform.forward *
                    (shoutInteractDistanceOuter / 2 + shoutInteractDistanceInner),
                    new Vector3(shoutWidthOuter / 2, shoutWidthOuter / 2, shoutInteractDistanceOuter / 2),
                    m_interactablesOuter, cameraTransform.rotation, interactableLayers, QueryTriggerInteraction.Ignore
                );

                if (outerLength != 0 || innerLength != 0)
                {
                    TestColliderArray(ref m_interactablesInner, ref innerLength);
                    TestColliderArray(ref m_interactablesOuter, ref outerLength);
                    if (m_unblockedInteractables.Count != 0) m_unblockedInteractables.First().Value.OnGettingShouted();
                    m_unblockedInteractables.Clear();
                    m_singleInteractables.Clear();
                }
            }
        }

        private void TestColliderArray(ref Collider[] colliders, ref int length)
        {
            var cameraTransform = PlayerManager.Instance.PlayerCamera.transform;
            var colliderTable = ColliderTable.Instance;
            for (var i = 0; i < length; i++)
            {
                if (!colliderTable.TryGetValue(colliders[i], out var extender)) continue;
                var interactable = extender.ShoutTrigger;
                if (!interactable) continue;
                var distance = Vector3.Distance(cameraTransform.position, interactable.transform.position);
                if (UnityEngine.Physics.Raycast(cameraTransform.position,
                        interactable.transform.position - cameraTransform.position,
                        distance, terrainLayers, QueryTriggerInteraction.Ignore)) continue;
                if (m_singleInteractables.Add(interactable)) m_unblockedInteractables.Add(distance, interactable);
            }
        }

        private void PlayShoutAudio() => shoutPlayer.Play(shoutAudios);

        private void PlayShoutAnimation()
        {
            var playerCameraTransform = PlayerManager.Instance.PlayerCamera.transform;
            shoutParticles.transform.rotation = playerCameraTransform.rotation;
            shoutParticles.Play();
            CinematicManager.Instance.ShakeCameraFOV(CameraManager.Instance.Camera.gameObject
                .GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera, 0.5f);
        }

        private void Start() => GameManager.Instance.AddFrameUpdate(this);

        private void OnDestroy() => GameManager.Instance?.RemoveFrameUpdate(this);
    }
}