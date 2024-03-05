using System;
using SFX;
using UnityEngine;

namespace Player
{
    public class PlayerFlashlightController : MonoBehaviour
    {
        private bool isEnableLight;
        private Light flashlight;
        private Animator flashlightAnimator;

        private void Start()
        {
            flashlightAnimator = GetComponent<Animator>();
            flashlight = GetComponentInChildren<Light>();
            flashlight.enabled = false;
        }

        public void SwitchFlashLight()
        {
            isEnableLight = !isEnableLight;
            flashlight.enabled = isEnableLight;
            PlayerAudioManager.instance.PlaySFX(AudioCollection.Click);
        }

        public void SwitchFlashlightAnimation(bool isRun)
        {
            // flashlightAnimator.SetBool(StringAnimCollection.isRun, isRun);
            flashlightAnimator.speed = isRun ? 1f : 0.3f;
        }
    }
}
