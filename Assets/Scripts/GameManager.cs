using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.Zombie;
using UnityEngine;

public enum GameScenarioLevel
{
    Morgue,
    BasementWithoutElectricity,
    BurstPipe
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private ZombieActivator zombieActivator;
    [SerializeField] private GameObject decorativeZombie;
    private GameScenarioLevel gameScenarioLevel;

    private void Start()
    {
        gameScenarioLevel = GameScenarioLevel.Morgue;
    }

    public void SetNewScenarioStage(GameScenarioLevel newLevel)
    {
        gameScenarioLevel = newLevel;
    }

    public void ActivateNewZombieGroup()
    {
        zombieActivator.ActivateNewGroupOfZombies();
        RemoveDecorativeZombie();
        
    }

    private void RemoveDecorativeZombie()
    {
        decorativeZombie.SetActive(false);
    }
}
