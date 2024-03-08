using System;
using Interactable;
using UnityEngine;
using MenuScene;

namespace Lightning
{
    public class LightSwitchController : MonoBehaviour, IInteractable
    {
        private string interactionMessageEn = "press E to switch the light";
        private string interactionMessageUa = "натисніть Е, щоб увімкнути світло";
        private string actualMessage;
        [SerializeField] private Light[] connectingLights;
        [SerializeField] private bool isEnable;
        private Animator switchAnimator;
        private AudioSource audioSource;

        private void Start()
        {
            CheckLocalization();
            audioSource = GetComponent<AudioSource>();
            switchAnimator = GetComponent<Animator>();
        }

        public string GetInteractionPlayerMessage()
        {
            return actualMessage;
        }

        public void ActivateAction()
        {
            SwitchLight();
        }

        public void CheckLocalization()
        {
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                actualMessage = interactionMessageEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                actualMessage = interactionMessageUa;
            }
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