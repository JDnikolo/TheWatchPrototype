using UnityEngine;
using UnityEngine.Serialization;

namespace Interactables.Actions.Animation
{
public class ToggleLights : Interactable
{

    [SerializeField] private ParticleSystem particles;
    [FormerlySerializedAs("Component")] [FormerlySerializedAs("light")] [SerializeField]
    private Light lightComponent ;
    [SerializeField] private MeshRenderer sphere;

    private bool m_smoke = true;
    
    public override void Interact()
    {
        switch (m_smoke)
        {
            case true when lightComponent.intensity > 0:
                particles.Stop();
                lightComponent.intensity = 0;
                sphere.enabled = false;
                m_smoke = !m_smoke;
                break;
            case false when lightComponent.intensity <= 0:
                particles.Play();
                lightComponent.intensity = 2;  
                sphere.enabled = true;
                m_smoke = !m_smoke;
                break;
        }
    }
}
}