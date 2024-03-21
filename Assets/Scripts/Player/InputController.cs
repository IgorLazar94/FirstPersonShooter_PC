using System;
using UnityEngine;

namespace Player
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private DynamicCanvasController dynamicCanvas;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private PlayerFlashlightController flashlight;
        [SerializeField] private AudioSource footstepAudioSource;
        [SerializeField] private AudioClip walkSound;

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

            if (Input.GetMouseButtonDown(1))
            {
                weaponController.MeleeAttack();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (dynamicCanvas.IsEnableNotePanel)
                {
                    dynamicCanvas.HideNote();
                }
                else
                {
                    gameManager.SwitchPause();
                }
            }
        }

        private void CheckWalkSound()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    footstepAudioSource.pitch = 2f;
                    footstepAudioSource.volume = 1f;
                    flashlight.SwitchFlashlightAnimation(true);
                }
                else
                {
                    flashlight.SwitchFlashlightAnimation(false);
                    footstepAudioSource.pitch = 1f;
                    footstepAudioSource.volume = 0.5f;
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