using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

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
        Invoke(nameof(CalculateDamage), 0.5f); // invoke for attack
    }

    private void CalculateDamage()
    {
        if (maxPlayerHealth <= 0)
        {
            Debug.Log("player death");
        }
        postProcessorController.UpdateDamageEffect(maxPlayerHealth);
    }

    public void AddPlayerHealth(int healthPoints)
    {
        playerHealth += healthPoints;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHealth);
    }

    public void AddPlayerAmmo(int ammoCount)
    {
        weaponController.AddBulletsToInventory(ammoCount);
    }
}
