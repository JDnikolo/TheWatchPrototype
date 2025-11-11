using System;
using System.ComponentModel;
using Agents.Behaviors;
using Character;
using Managers;
using UnityEditor;
using UnityEngine;
using Utilities;
using Animation;

public class PersonAnimationController : MonoBehaviour, IFixedUpdatable
{
  
   [SerializeField] private Animator animator;
   

   [SerializeField] private bool blendIdle = true;
   [SerializeReference] private AnimationBlender animationBlender;
   
   public byte UpdateOrder { get; } = byte.MaxValue;

   public void OnFixedUpdate()
   {
      if (blendIdle && animationBlender) animationBlender.SetBlendValues();
   }

   private void Awake() => GameManager.Instance.AddFixedUpdateSafe(this);


   private void OnDestroy() => GameManager.Instance.RemoveFixedUpdateSafe(this);
}
