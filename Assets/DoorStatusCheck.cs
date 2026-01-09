using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables.Actions.Animation
{

    public class DoorStatusCheck : Interactable
    {


        [SerializeField] private Animator doorAnimator;
        private void Start()
        {
            doorAnimator.SetBool("IsOpen", true);
            Debug.Log(doorAnimator.GetBool("IsOpen"));
        }

        public override void Interact()
        {
            bool isOpen = doorAnimator.GetBool("IsOpen");
            doorAnimator.SetBool("IsOpen", !isOpen);
        }


    }
}
    
