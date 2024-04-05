using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Interactable;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MenuScene;
using ModularFirstPersonController.FirstPersonController;
using Player;
using SFX;
using Zenject;

public class DynamicCanvasController : MonoBehaviour
{
    public bool IsEnableNotePanel { get; private set; }

    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Transform questTextContainer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ExitDoor exitDoor;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private TextMeshProUGUI textInteractableMessage;
    [SerializeField] private TextMeshProUGUI bulletsLoadedInPistol;
    [SerializeField] private TextMeshProUGUI bulletsInPlayerInventory;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Animation crosshairMovedAnim;
    [SerializeField] private Animation hitDetectAnim;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private GameObject notePanel;
    [SerializeField] private GameObject hUDPanel;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private TextMeshProUGUI noteText;
    [SerializeField] private TextMeshProUGUI creditTMPText;
    [SerializeField] private Image[] crosshairLines;
    [TextArea(5, 10)] [SerializeField] private string creditTextEn;
    [TextArea(5, 10)] [SerializeField] private string creditTextUa;
    [SerializeField] private TextMeshProUGUI noteInstrText;
    [SerializeField] private Button backToMenuButton;
    private string actualNoteText;
    private string noteTextEn = "Press «Esc» to close the note";
    private string noteTextUa = "Натисніть «Esc», щоб закрити записку";
    private string backToMenuEn = "Back to menu";
    private string backToMenuUa = "Назад в меню";
    private string creditActualText;
    private Animation treatmentAnimation;
    private int lastBulletsInPistol;
    private int lastBulletsInInventory;
    private TextMeshProUGUI backToMenuText;
    private LocalizationController localizationController;
        
    [Inject]
    private void Construct(LocalizationController localizationController)
    {
        this.localizationController = localizationController;
    }

    private void OnEnable()
    {
        PlayerFlashlightController.OnEnableFlashlight += SwitchCrosshairLines;
    }

    private void OnDisable()
    {
        PlayerFlashlightController.OnEnableFlashlight -= SwitchCrosshairLines;
    }

    private void Start()
    {
        IsEnableNotePanel = false;
        treatmentAnimation = GetComponent<Animation>();
        backToMenuText = backToMenuButton.GetComponentInChildren<TextMeshProUGUI>();
        CheckLocalization();
    }

    public void SwitchPausePanel(bool isEnable)
    {
        firstPersonController.ToggleSprintBarVisibility(isEnable);
        pausePanel.gameObject.SetActive(isEnable);
        PlayerAudioManager.instance.PlaySFX(AudioCollection.ClickSound);
    }

    public void ResumeGame() // OnClickEvent
    {
        gameManager.SwitchPause();
        firstPersonController.ToggleSprintBarVisibility(false);
    }
    
    public void BackToMenu() // OnClickEvent
    {
        gameManager.BackToMenu();
    }

    public void UpdateTextMessage(string message)
    {
        textInteractableMessage.text = message;
    }

    public void UpdateBullets(int bulletsInPistol, int bulletsInInventory)
    {
        if (lastBulletsInPistol != bulletsInPistol)
        {
            bulletsLoadedInPistol.rectTransform.DOShakeAnchorPos(0.25f, Vector2.one * 10, 5, 180f);
        }

        if (lastBulletsInInventory != bulletsInInventory)
        {
            bulletsInPlayerInventory.rectTransform.DOShakeAnchorPos(0.25f, Vector2.one * 10, 5, 180f);
        }

        bulletsLoadedInPistol.text = bulletsInPistol.ToString();
        bulletsInPlayerInventory.text = bulletsInInventory.ToString();
        lastBulletsInPistol = bulletsInPistol;
        lastBulletsInInventory = bulletsInInventory;
    }

    public void ActivateDeathPanel()
    {
        firstPersonController.ToggleSprintBarVisibility(true);
        deathPanel.gameObject.SetActive(true);
        deathPanel.GetComponent<Image>().DOFade(1f, 1f).SetUpdate(true);
    }

    public void ReloadLevel() // OnClick event
    {
        SceneManager.LoadScene("AsylumLevel");
    }

    public void ActivateTreatmentEffect()
    {
        treatmentAnimation.Play();
    }

    public void PlayCrosshairAnim()
    {
        crosshairMovedAnim.Play();
    }

    public void PlayDetectAnim()
    {
        hitDetectAnim.Play();
    }

    public void UpdatePlayerHealthText(int hp)
    {
        if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
        {
            playerHealthText.text = "Health points: " + hp;
        }
        else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
        {
            playerHealthText.text = "Здоров'я гравця: " + hp;
        }

        playerHealthText.rectTransform.DOShakeAnchorPos(0.25f, Vector2.one * 10, 5, 180f);
        if (hp >= 70)
        {
            playerHealthText.color = Color.green;
        }
        else if (hp > 30)
        {
            playerHealthText.color = Color.yellow;
        }
        else
        {
            playerHealthText.color = Color.red;
        }
    }

    private void CheckLocalization()
    {
        if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
        {
            creditActualText = creditTextEn;
            backToMenuText.text = backToMenuEn;
            actualNoteText = noteTextEn;
        }
        else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
        {
            creditActualText = creditTextUa;
            backToMenuText.text = backToMenuUa;
            actualNoteText = noteTextUa;
        }
    }

    public void ShowNote(string newText)
    {
        questTextContainer.gameObject.SetActive(false);
        noteInstrText.text = actualNoteText;
        IsEnableNotePanel = true;
        hUDPanel.SetActive(false);
        gameManager.EnablePause();
        notePanel.transform.localScale = Vector3.zero;
        notePanel.gameObject.SetActive(true);
        noteText.text = newText;
        notePanel.transform.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        firstPersonController.ToggleSprintBarVisibility(true);
    }

    public void HideNote()
    {
        questTextContainer.gameObject.SetActive(true);
        notePanel.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).OnComplete(HideNotePanel);
    }

    private void HideNotePanel()
    {
        gameManager.ResumePauseGame();
        IsEnableNotePanel = false;
        hUDPanel.SetActive(true);
        notePanel.gameObject.SetActive(false);
        firstPersonController.ToggleSprintBarVisibility(false);
    }

    public void ShowCreditText()
    {
        firstPersonController.ToggleSprintBarVisibility(true);
        exitDoor.GetComponent<AudioSource>().Play();
        questTextContainer.gameObject.SetActive(false);
        hUDPanel.SetActive(false);
        firstPersonController.enabled = false;
        creditPanel.SetActive(true);
        gameManager.EnablePause();
        creditPanel.GetComponent<Image>().DOFade(1f, 0.5f).SetUpdate(true);
        StartCoroutine(SlowlyShowCreditTextWithSound());
    }

    private IEnumerator SlowlyShowCreditTextWithSound()
    {
        var elapsedTime = 0f;
        var characterDelay = 0.05f;

        foreach (var text in creditActualText)
        {
            creditTMPText.text += text;
            while (elapsedTime < characterDelay)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            elapsedTime = 0f;
        }
        exitDoor.GetComponent<AudioSource>().Stop();
        backToMenuButton.gameObject.SetActive(true);
        gameManager.UnlockCursor(false);
    }

    private void SwitchCrosshairLines(bool withFlashlight)
    {
        foreach (var line in crosshairLines)
        {
            if (withFlashlight)
            {
                line.color = Color.green;
            }
            else
            {
                line.color = Color.white;
            }
        }
    }
}