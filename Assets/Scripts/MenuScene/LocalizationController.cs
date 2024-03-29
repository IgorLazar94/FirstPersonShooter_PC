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
        private TypeOfLocalization currentLocalization;
        private readonly TypeOfLocalization defaultLocalization = TypeOfLocalization.Ukrainian;

        private void Awake()
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

        public TypeOfLocalization GetCurrentLocalization()
        {
            return currentLocalization;
        }
    }
}