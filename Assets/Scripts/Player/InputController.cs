using System;
using UnityEngine;

namespace Player
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private PlayerFlashlightController flashlight;
        [SerializeField] private AudioSource footstepAudioSource;
        [SerializeField] private AudioClip walkSound;
        private AudioLowPassFilter lowPassFilter;

        private void Start()
        {
            lowPassFilter = GetComponent<AudioLowPassFilter>();
        }

        private void Update()
        {
            CheckWalkSound();
            
            if (Input.GetMouseButtonDown(0))
            {
                weaponController.Shot();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                weaponController.Reload();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                weaponController.SwitchRunState(true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                weaponController.SwitchRunState(false);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlight.SwitchFlashLight();
            }
        }

        private void CheckWalkSound()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    footstepAudioSource.pitch = 2f;
                    lowPassFilter.cutoffFrequency = 500f;
                    flashlight.SwitchFlashlightAnimation(true);
                }
                else
                {
                    flashlight.SwitchFlashlightAnimation(false);
                    footstepAudioSource.pitch = 1f;
                }

                if (!footstepAudioSource.isPlaying)
                {
                    footstepAudioSource.PlayOneShot(walkSound);
                }
            }
            else
            {
                footstepAudioSource.Stop();
            }
        }
    }
}