using System;
using Enemy.Zombie;
using PauseSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

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
    [SerializeField] private DynamicCanvasController dynamicCanvas;
    [SerializeField] private FirstPersonController firstPersonController;
    private GameScenarioLevel gameScenarioLevel;
    private IPauseService pauseService;

    [Inject]
    private void Construct(PauseService pauseService)
    {
        this.pauseService = pauseService;
    }

    private void Start()
    {
        Time.timeScale = 1;
        gameScenarioLevel = GameScenarioLevel.Morgue;
        // SetPauseService(new PauseService());
        pauseService = new PauseService();
    }
    
    // private void SetPauseService(IPauseService pauseService)
    // {
    //     this.pauseService = pauseService;
    // }

    public void SetNewScenarioStage(GameScenarioLevel newLevel)
    {
        gameScenarioLevel = newLevel;
        QuestSystem.OnUpdateQuest?.Invoke();
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

    public void SwitchPause()
    {
        if (pauseService.IsPaused)
        {
            ResumePauseGame();
        }
        else
        {
            EnablePause();
        }

        firstPersonController.enabled = !pauseService.IsPaused;
        UnlockCursor(!pauseService.IsPaused);
        dynamicCanvas.SwitchPausePanel(pauseService.IsPaused);
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
