using System;
using System.Collections.Generic;
using System.Linq;
using Interactables;
using Managers.Persistent;
using Runtime.FixedUpdate;
using UnityEngine;
using Utilities;

namespace Animation
{
   public class PersonAnimationController : MonoBehaviour, IFixedUpdatable
   {

      [SerializeField] private Animator animator;

      [SerializeField] private bool blendIdle = true;
      [SerializeReference] private AnimationBlender animationBlender;

      private Dictionary<int, float> m_animationTimers = new();
      private Dictionary<int, Interactable> m_animationFinishedCallbacks = new();

      public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.Animation;

      public void OnFixedUpdate()
      {
         if (blendIdle && animationBlender) animationBlender.SetBlendValues();

         if (m_animationTimers.Count == 0) return;
         foreach (var key in m_animationTimers.Keys.ToList())
         {
            m_animationTimers[key] -= Time.fixedDeltaTime;

            if (!(m_animationTimers[key] <= 0)) continue;
            StopAnimation(key);
         }
      }

      public void StartAnimation(int animationHash, float duration = -1.0f, Interactable callback = null)
      {
         var parameter = animator.parameters.FirstOrDefault(param => param.nameHash == animationHash);

         if (parameter == null)
         {
            Debug.LogWarning("Animation not found: " + animationHash);
            return;
         }

         switch (parameter.type)
         {
            case AnimatorControllerParameterType.Trigger:
               animator.SetTrigger(animationHash);
               break;
            case AnimatorControllerParameterType.Bool:
               animator.SetBool(animationHash, !parameter.defaultBool);
               break;
            case AnimatorControllerParameterType.Float:
            case AnimatorControllerParameterType.Int:
               Debug.LogWarning("Use SetAnimationParameter() for float and int parameters");
               return;
            default:
               throw new ArgumentOutOfRangeException();
         }

         if (!(duration > 0)) return;

         if (!m_animationTimers.TryAdd(animationHash, duration))
         {

            m_animationTimers[animationHash] = duration;
         }

         if (callback is null) return;

         if (!m_animationFinishedCallbacks.TryAdd(animationHash, callback))
         {
            m_animationFinishedCallbacks[animationHash] = callback;
         }
      }

      public void StopAnimation(int animationHash, bool callOnFinish = true)
      {
         var parameter = animator.parameters.FirstOrDefault(param => param.nameHash == animationHash);

         if (parameter == null)
         {
            Debug.LogWarning("Animation not found: " + animationHash);
            return;
         }

         switch (parameter.type)
         {
            case AnimatorControllerParameterType.Trigger:
               break;
            case AnimatorControllerParameterType.Bool:
               animator.SetBool(animationHash, parameter.defaultBool);
               break;
            case AnimatorControllerParameterType.Float:
               animator.SetFloat(animationHash, parameter.defaultFloat);
               break;
            case AnimatorControllerParameterType.Int:
               animator.SetInteger(animationHash, parameter.defaultInt);
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }

         if (m_animationTimers.ContainsKey(animationHash)) m_animationTimers.Remove(animationHash);

         if (m_animationFinishedCallbacks.ContainsKey(animationHash))
         {
            var callback = m_animationFinishedCallbacks[animationHash];
            m_animationFinishedCallbacks.Remove(animationHash);
            if (callOnFinish) callback.Interact();

         }
      }

      public void SetAnimationParameter(int parameterHash, float value, float duration = -1.0f,
         Interactable callback = null)
      {
         var parameter = animator.parameters.FirstOrDefault(param => param.nameHash == parameterHash);

         if (parameter == null)
         {
            Debug.LogWarning("Animation parameter not found: " + parameterHash);
            return;
         }

         animator.SetFloat(parameterHash, value);

         if (!(duration > 0)) return;

         if (!m_animationTimers.TryAdd(parameterHash, duration)) m_animationTimers[parameterHash] = duration;

         if (!m_animationFinishedCallbacks.TryAdd(parameterHash, callback))
         {
            m_animationFinishedCallbacks[parameterHash] = callback;
         }
      }

      public void SetAnimationParameter(int parameterHash, int value, float duration = -1.0f,
         Interactable callback = null)
      {
         var parameter = animator.parameters.FirstOrDefault(param => param.nameHash == parameterHash);

         if (parameter == null)
         {
            Debug.LogWarning("Animation parameter not found: " + parameterHash);
            return;
         }

         animator.SetInteger(parameterHash, value);

         if (!(duration > 0)) return;

         if (!m_animationTimers.TryAdd(parameterHash, duration)) m_animationTimers[parameterHash] = duration;

         if (!m_animationFinishedCallbacks.TryAdd(parameterHash, callback))
         {
            m_animationFinishedCallbacks[parameterHash] = callback;
         }
      }

      private void Awake() => GameManager.Instance.AddFixedUpdateSafe(this);


      private void OnDestroy() => GameManager.Instance.RemoveFixedUpdateSafe(this);
   }
}