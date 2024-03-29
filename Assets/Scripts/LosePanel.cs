using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MenuScene;
using  Zenject;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button backToMenuBtn;

    private string actualTitleText;
    private string actualRestartBtnText;
    private string actualBackToMenuBtnText;

    private string titleTextEn = "You Dead";
    private string restartBtnTextEn = "Restart";
    private string backToMenuBtnTextEn = "Back to menu";
    
    private string titleTextUa = "Ти загинув";
    private string restartBtnTextUa = "Рестарт";
    private string backToMenuBtnTextUa = "В меню";
    private LocalizationController localizationController;
        
    [Inject]
    private void Construct(LocalizationController localizationController)
    {
        this.localizationController = localizationController;
    }

    private void Awake()
    {
        CheckLocalization();
        UpdateLosePanelText();
    }

    private void CheckLocalization()
    {
        if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
        {
            actualTitleText = titleTextEn;
            actualBackToMenuBtnText = backToMenuBtnTextEn;
            actualRestartBtnText = restartBtnTextEn;

        }
        else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
        {
            actualTitleText = titleTextUa;
            actualBackToMenuBtnText = backToMenuBtnTextUa;
            actualRestartBtnText = restartBtnTextUa;
        }
    }
    
    private void UpdateLosePanelText()
    {
        titleText.text = actualTitleText;
        backToMenuBtn.GetComponentInChildren<TextMeshProUGUI>().text = actualBackToMenuBtnText;
        restartBtn.GetComponentInChildren<TextMeshProUGUI>().text = actualRestartBtnText;
    }
}
