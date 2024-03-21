using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MenuScene;

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

    private void Awake()
    {
        CheckLocalization();
        UpdateLosePanelText();
    }

    private void CheckLocalization()
    {
        if (LocalizationController.currentLocalization == TypeOfLocalization.English)
        {
            actualTitleText = titleTextEn;
            actualBackToMenuBtnText = backToMenuBtnTextEn;
            actualRestartBtnText = restartBtnTextEn;

        }
        else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
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
