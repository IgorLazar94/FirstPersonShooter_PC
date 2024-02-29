using System;
using Interactable;
using UnityEngine;

namespace Lightning
{
    public class LightSwitchController : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactionMessage = "press E to switch the light";
        [SerializeField] private Light[] connectingLights;
        [SerializeField] private bool isEnable;
        private Animator switchAnimator;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            switchAnimator = GetComponent<Animator>();
        }

        public string GetInteractionPlayerMessage()
        {
            return interactionMessage;
        }

        public void ActivateAction()
        {
            SwitchLight();
        }

        private void SwitchLight()
        {
            isEnable = !isEnable;
            foreach (var light in connectingLights)
            {
                light.enabled = isEnable;
            }
            ActivateSwitchAnimation(isEnable);
        }

        private void ActivateSwitchAnimation(bool isEnabled)
        {
            switchAnimator.SetTrigger(StringAnimCollection.Switch);
            switchAnimator.SetBool(StringAnimCollection.IsActive, isEnabled);
            if (isEnabled)
            {
                audioSource.Play();
            }
        }
    }
}