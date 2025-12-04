using System;
using System.Collections.Generic;
using Animation;
using UnityEditor;
using UnityEngine;

namespace Interactables.Actions.Animation
{
    [AddComponentMenu("Interactables/Animation/Set Animation Parameter")]
    public class InteractableSetAnimationParameter : Interactable
    {
        [SerializeReference] private PersonAnimationController animationController;
        
        [SerializeField] private AnimatorControllerParameterType parameterType = AnimatorControllerParameterType.Float;
        
        [SerializeField] private string parameterName;
        
        [SerializeField] private float parameterValueFloat;
        
        [SerializeField] private int parameterValueInt;
        
        [SerializeField] private bool parameterValueBool;
        
        [SerializeField] private float duration = -1.0f;
        
        [SerializeField] private Interactable onAnimationEnd = null;

        public override void Interact()
        {
            var parameterHash = Animator.StringToHash(parameterName);
            switch (parameterType)
            {
                case AnimatorControllerParameterType.Float:
                    animationController.SetAnimationParameter(parameterHash, parameterValueFloat, duration, onAnimationEnd);
                    break;
                case AnimatorControllerParameterType.Int:
                    animationController.SetAnimationParameter(parameterHash, parameterValueInt, duration, onAnimationEnd);
                    break;
                case AnimatorControllerParameterType.Bool:
                    if (parameterValueBool)
                    {
                        animationController.StartAnimation(parameterHash, duration, onAnimationEnd);
                    }
                    else
                    {
                        animationController.StopAnimation(parameterHash);
                    }
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animationController.StartAnimation(parameterHash, duration, onAnimationEnd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        #if UNITY_EDITOR
                [CustomEditor(typeof(InteractableSetAnimationParameter))]
                class ISAPEditor : Editor
                {
                    public override void OnInspectorGUI()
                    {
                        var self = (InteractableSetAnimationParameter)target;
                        serializedObject.Update();
                        var propertiesToExclude = new List<string>{"parameterValueFloat", "parameterValueInt", "parameterValueBool" };
                        
                        switch (self.parameterType)
                        {
                            case AnimatorControllerParameterType.Float:
                                propertiesToExclude.Remove("parameterValueFloat");
                                break;
                            case AnimatorControllerParameterType.Int:
                                propertiesToExclude.Remove("parameterValueInt");
                                break;
                            case AnimatorControllerParameterType.Bool:
                                propertiesToExclude.Remove("parameterValueBool");
                                break;
                            case AnimatorControllerParameterType.Trigger:
                                break;
                            default:
                                break;
                        }
                        DrawPropertiesExcluding(serializedObject,propertiesToExclude.ToArray());
                        serializedObject.ApplyModifiedProperties();
                    }
                }
        #endif
    }
}