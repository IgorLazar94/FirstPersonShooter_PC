using MenuScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button resumeGameButton;
    
    private string actualTitleText;
    private string actualBackToMenuBtnText;
    private string actualResumeGameBtnText;

    private string titleTextEn = "Game on pause";
    private string backToMenuEn = "Back to menu";
    private string resumeGameEn = "Resume game";
    private string titleTextUa = "Гра на паузі";
    private string backToMenuUa = "Назад в меню";
    private string resumeGameUa = "Відновити гру";
    private LocalizationController localizationController;
        
    [Inject]
    private void Construct(LocalizationController localizationController)
    {
        this.localizationController = localizationController;
    }
    private void Awake()
    {
        CheckLocalization();
        UpdatePausePanelText();
    }


    private void CheckLocalization()
    {
        if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
        {
            actualTitleText = titleTextEn;
            actualBackToMenuBtnText = backToMenuEn;
            actualResumeGameBtnText = resumeGameEn;

        }
        else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
        {
            actualTitleText = titleTextUa;
            actualBackToMenuBtnText = backToMenuUa;
            actualResumeGameBtnText = resumeGameUa;
        }
    }

    private void UpdatePausePanelText()
    {
        titleText.text = actualTitleText;
        backToMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = actualBackToMenuBtnText;
        resumeGameButton.GetComponentInChildren<TextMeshProUGUI>().text = actualResumeGameBtnText;
    }
}