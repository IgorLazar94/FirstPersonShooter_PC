using System;
using TMPro;
using UnityEngine;

namespace MenuScene
{
    public class ControlText : MonoBehaviour
    {
        [SerializeField] private string englishText;
        [SerializeField] private string ukrainianText;
        private TextMeshProUGUI controlText;

        private void Awake()
        {
            controlText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            LocalizationController.onLanguageChanged += SwitchLanguage;
        }

        private void OnDisable()
        {
            LocalizationController.onLanguageChanged -= SwitchLanguage;
        }

        private void SwitchLanguage(TypeOfLocalization typeOfLocalization)
        {
            switch (typeOfLocalization)
            {
                case TypeOfLocalization.English :
                    controlText.text = englishText;
                    break;
                case TypeOfLocalization.Ukrainian :
                    controlText.text = ukrainianText;
                    break;
            }
        }

        public void CheckLanguage()
        {
            switch (LocalizationController.currentLocalization)
            {
                case TypeOfLocalization.English :
                    controlText.text = englishText;
                    break;
                case TypeOfLocalization.Ukrainian :
                    controlText.text = ukrainianText;
                    break;
            }
        }
    }
}
