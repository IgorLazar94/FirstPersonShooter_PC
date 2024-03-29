using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MenuScene
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject controlPanel;
        [SerializeField] private LoadControl loadPanel;
        [SerializeField] private ParticleSystem[] menuEffects;
        private ControlText[] controlTexts;
        private MenuSceneButton backButton;
        private LocalizationController localizationController;
        
        [Inject]
        private void Construct(LocalizationController localizationController)
        {
            this.localizationController = localizationController;
        }

        private void Start()
        {
            LocalizationController.onLanguageChanged?.Invoke(localizationController.GetCurrentLocalization());
            Time.timeScale = 1;
            PlayMenuEffects();
            controlTexts = controlPanel.GetComponentsInChildren<ControlText>();
            backButton = controlPanel.GetComponentInChildren<MenuSceneButton>();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void PlayMenuEffects()
        {
            foreach (ParticleSystem fx in menuEffects)
            {
                fx.Play();
            }
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
            backButton.SwitchButtonText(localizationController.GetCurrentLocalization());
        }

        public void LoadGameplayScene() // OnClickEvent
        {
            loadPanel.gameObject.SetActive(true);
            loadPanel.InitLoadPanel();
            Cursor.visible = false;
            SceneManager.LoadSceneAsync("AsylumLevel");
        }

        public void SwitchLanguageFromButton() // OnClickEvent
        {
            localizationController.SwitchLanguageToAnother();
        }

        public void Exit() // OnClickEvent
        {
            Application.Quit();
        }
    }
}
