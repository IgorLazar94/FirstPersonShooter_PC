using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameScenarioLevel
{
    Morgue,
    BasementWithoutElectricity,
    BurstPipe
}
public class GameManager : MonoBehaviour
{
    private GameScenarioLevel gameScenarioLevel;

    private void Start()
    {
        gameScenarioLevel = GameScenarioLevel.Morgue;
    }

    public void SetNewScenarioStage(GameScenarioLevel newLevel)
    {
        gameScenarioLevel = newLevel;
    }
}
