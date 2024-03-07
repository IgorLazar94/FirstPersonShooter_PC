using System;
using DG.Tweening;
using SFX;
using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private PostProcController postProcessorController;
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private DynamicCanvasController dynamicCanvas;
        private FirstPersonController firstPersonController;
        private int maxPlayerHealth = 100;
        private int playerHealth;

        private void Start()
        {
            playerHealth = maxPlayerHealth;
            UpdateHealthBarOnCanvas();
            firstPersonController = GetComponent<FirstPersonController>();
        }

        public void PlayerTakeDamage(int damage)
        {
            playerHealth -= damage;
            playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHealth);
            UpdateHealthBarOnCanvas();
            PlayerAudioManager.instance.PlaySFX(AudioCollection.PlayerTakeDamage);
            Invoke(nameof(CheckPlayerHp), 0.5f); // invoke for attack
        }

        private void CheckPlayerHp()
        {
            if (playerHealth <= 0)
            {
                GetComponent<InputController>().enabled = false;
                GetComponent<FirstPersonController>().enabled = false;
                weaponController.transform.DOMoveY(-6f, 0.25f);
                Invoke(nameof(ActivateDeadEffect), 1.0f);
            }

            postProcessorController.UpdateDamageEffect(playerHealth);
        }

        private void ActivateDeadEffect()
        {
            Time.timeScale = 0;
            dynamicCanvas.ActivateDeathPanel();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void AddPlayerHealth(int healthPoints)
        {
            playerHealth += healthPoints;
            playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHealth);
            UpdateHealthBarOnCanvas();
            PlayerAudioManager.instance.PlaySFX(AudioCollection.PickupFirstAidKit);
            dynamicCanvas.ActivateTreatmentEffect();
        }

        public void AddPlayerAmmo(int ammoCount)
        {
            weaponController.AddBulletsToInventory(ammoCount);
        }

        private void UpdateHealthBarOnCanvas()
        {
            dynamicCanvas.UpdatePlayerHealthText(playerHealth);
        }
    }
}