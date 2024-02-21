using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private PostProcController postProcessorController;
    private int playerHealth = 100;

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        Invoke(nameof(CalculateDamage), 0.5f); // invoke for attack
    }

    private void CalculateDamage()
    {
        if (playerHealth <= 0)
        {
            Debug.Log("player death");
        }
        postProcessorController.UpdateDamageEffect(playerHealth);
    }
}
