using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Interactables.Actions
{
    
 

public class ToggleLights : Interactable
{

    [SerializeField] ParticleSystem particles;
    [SerializeField] Light light ;
    [SerializeField] MeshRenderer sphere;

    float originalIntensity;
    bool smoke = true;
    public void start()
        {
            float originalIntensity = 0; 
        }    

    
    public override void Interact()
    {
       if (smoke && light.intensity > 0)
        {
            particles.Stop();
            light.intensity = 0;
            sphere.enabled = false;
            smoke = !smoke;
       }
        else if (!smoke && light.intensity <= 0)
        {
            particles.Play();
            light.intensity = 2;  
            sphere.enabled = true;
            smoke = !smoke;
        }
    }
}
}