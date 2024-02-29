using SFX;
using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private PostProcController postProcessorController;
        [SerializeField] private WeaponController weaponController;
        private int maxPlayerHealth = 100;
        private int playerHealth;

        private void Start()
        {
            playerHealth = maxPlayerHealth;
        }

        public void PlayerTakeDamage(int damage)
        {
            playerHealth -= damage;
            playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHealth);
            PlayerAudioManager.instance.PlaySFX(AudioCollection.PlayerTakeDamage);
            Invoke(nameof(CheckPlayerHp), 0.5f); // invoke for attack
        }

        private void CheckPlayerHp()
        {
            if (maxPlayerHealth <= 0)
            {
                Debug.Log("player death");
            }
            postProcessorController.UpdateDamageEffect(playerHealth);
        }

        public void AddPlayerHealth(int healthPoints)
        {
            playerHealth += healthPoints;
            playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHealth);
            PlayerAudioManager.instance.PlaySFX(AudioCollection.PickupFirstAidKit);
        }

        public void AddPlayerAmmo(int ammoCount)
        {
            weaponController.AddBulletsToInventory(ammoCount);
        }
    }
}
