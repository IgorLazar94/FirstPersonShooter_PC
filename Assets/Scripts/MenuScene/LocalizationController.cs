using System;
using UnityEngine;

namespace MenuScene
{
    public enum TypeOfLocalization
    {
        English,
        Ukrainian
    }

    public class LocalizationController : MonoBehaviour
    {
        public static Action<TypeOfLocalization> onLanguageChanged;
        public static TypeOfLocalization currentLocalization;
        private TypeOfLocalization defaultLocalization = TypeOfLocalization.English;

        private void Start()
        {
            SetLocalization(defaultLocalization);
        }

        private void SetLocalization(TypeOfLocalization localization)
        {
            currentLocalization = localization;
            onLanguageChanged?.Invoke(localization);
        }

        public void SwitchLanguageToAnother()
        {
            if (currentLocalization == TypeOfLocalization.English)
            {
                currentLocalization = TypeOfLocalization.Ukrainian;
            }
            else if (currentLocalization == TypeOfLocalization.Ukrainian)
            {
                currentLocalization = TypeOfLocalization.English;
            }
            onLanguageChanged?.Invoke(currentLocalization);
        }
    }
}