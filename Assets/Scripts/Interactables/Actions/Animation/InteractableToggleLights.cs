using UnityEngine;

namespace Interactables.Actions.Animation
{
    public sealed class InteractableToggleLights : Interactable
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private Light lightComponent;
        [SerializeField] private MeshRenderer sphere;

        private float m_intensity;
        private bool m_toggled = true;

        public override void Interact()
        {
            if (m_toggled)
            {
                particles.Stop();
                m_toggled = sphere.enabled = lightComponent.enabled = false;
            }
            else
            {
                particles.Play();
                m_toggled = sphere.enabled = lightComponent.enabled = true;   
            }
        }

        private void Start() => m_toggled = sphere.enabled;
    }
}