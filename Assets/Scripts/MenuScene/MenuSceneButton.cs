using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScene
{
    [RequireComponent(typeof(Button))]
    public class MenuSceneButton : MonoBehaviour
    {
        [SerializeField] private string ukrainianText;
        [SerializeField] private string englishText;
        private TextMeshProUGUI buttonText;

        private void OnEnable()
        {
            LocalizationController.onLanguageChanged += SwitchButtonText;
        }

        private void OnDisable()
        {
            LocalizationController.onLanguageChanged -= SwitchButtonText;
        }

        private void Awake()
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void SwitchButtonText(TypeOfLocalization typeOfLocalization)
        {
            switch (typeOfLocalization)
            {
                case TypeOfLocalization.English:
                    buttonText.text = englishText;
                    break;
                case TypeOfLocalization.Ukrainian:
                    buttonText.text = ukrainianText;
                    break;
            }
        }
        
        
    }
}
