using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScene
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject controlPanel;
        private ControlText[] controlTexts;
        private MenuSceneButton backButton;

        private void Start()
        {
            controlTexts = controlPanel.GetComponentsInChildren<ControlText>();
            backButton = controlPanel.GetComponentInChildren<MenuSceneButton>();
        }

        public void ActivateControlPanel(bool isActive) // OnClickEvent
        {
            controlPanel.SetActive(isActive);
            CheckLocalization();
        }

        private void CheckLocalization()
        {
            foreach (var controlText in controlTexts)
            {
                controlText.CheckLanguage();
            }
            backButton.SwitchButtonText(LocalizationController.currentLocalization);
        }

        public void LoadGameplayScene() // OnClickEvent
        {
            SceneManager.LoadScene("AsylumLevel");
        }
    }
}
