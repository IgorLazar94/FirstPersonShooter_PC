using System;
using Enemy.Zombie;
using PauseSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private IPauseService pauseService;

    private void Start()
    {
        gameScenarioLevel = GameScenarioLevel.Morgue;
        SetPauseService(new PauseService());
    }
    
    private void SetPauseService(IPauseService pauseService)
    {
        this.pauseService = pauseService;
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

    public void EnablePause()
    {
        pauseService.PauseGame();
    }

    public void ResumePauseGame()
    {
        pauseService.ResumeGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void UnlockCursor(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
