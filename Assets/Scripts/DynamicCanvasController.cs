using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MenuScene;
using Player;
using SFX;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class DynamicCanvasController : MonoBehaviour
{
    public bool IsEnableNotePanel { get; private set; }

    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private GameManager gameManager;
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
    [SerializeField] private TextMeshProUGUI noteText;
    [SerializeField] private TextMeshProUGUI creditTMPText;
    [SerializeField] private Image[] crosshairLines;
    [TextArea(5, 10)] [SerializeField] private string creditTextEn;
    [TextArea(5, 10)] [SerializeField] private string creditTextUa;
    [SerializeField] private Button backToMenuButton;
    private string backToMenuEn = "Back to menu";
    private string backToMenuUa = "Назад в меню";
    private string creditActualText;
    private Animation treatmentAnimation;
    private int lastBulletsInPistol;
    private int lastBulletsInInventory;
    private TextMeshProUGUI backToMenuText;

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
        deathPanel.gameObject.SetActive(true);
        deathPanel.GetComponent<Image>().DOFade(1f, 1f).SetUpdate(true);
    }

    public void ReloadLevel() // OnClick event
    {
        SceneManager.LoadScene(0);
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
        if (LocalizationController.currentLocalization == TypeOfLocalization.English)
        {
            playerHealthText.text = "Health points: " + hp;
        }
        else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
        {
            playerHealthText.text = "Здоров'я ігрока: " + hp;
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
        if (LocalizationController.currentLocalization == TypeOfLocalization.English)
        {
            creditActualText = creditTextEn;
            backToMenuText.text = backToMenuEn;
        }
        else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
        {
            creditActualText = creditTextUa;
            backToMenuText.text = backToMenuUa;
        }
    }

    public void ShowNote(string newText)
    {
        IsEnableNotePanel = true;
        hUDPanel.SetActive(false);
        gameManager.EnablePause();
        notePanel.transform.localScale = Vector3.zero;
        notePanel.gameObject.SetActive(true);
        noteText.text = newText;
        notePanel.transform.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        firstPersonController.enabled = false;
    }

    public void HideNote()
    {
        notePanel.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).OnComplete(HideNotePanel);
    }

    private void HideNotePanel()
    {
        gameManager.ResumePauseGame();
        IsEnableNotePanel = false;
        hUDPanel.SetActive(true);
        notePanel.gameObject.SetActive(false);
        firstPersonController.enabled = true;
    }

    public void ShowCreditText()
    {
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
            PlayRandomTypeWriterSound();
            while (elapsedTime < characterDelay)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            elapsedTime = 0f;
        }

        backToMenuButton.gameObject.SetActive(true);
        gameManager.UnlockCursor(false);
    }

    public void BackToMenu() // OnClick
    {
        gameManager.BackToMenu();
    }

    private void PlayRandomTypeWriterSound()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                PlayerAudioManager.instance.PlaySFX(AudioCollection.TypeWriterPitched1);
                break;
            case 1:
                PlayerAudioManager.instance.PlaySFX(AudioCollection.TypeWriterPitched2);
                break;
            case 2:
                PlayerAudioManager.instance.PlaySFX(AudioCollection.TypeWriterPitched3);
                break;
        }
    }

    private void SwitchCrosshairLines(bool withFlashlight)
    {
        foreach (var line in crosshairLines)
        {
            if (withFlashlight)
            {
                line.color = Color.HSVToRGB(98f, 98f, 98f);
            }
            else
            {
                line.color = Color.white;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            BackToMenu();
        }
    }
}