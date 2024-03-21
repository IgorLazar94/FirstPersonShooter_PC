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
        private static LocalizationController _instance;
        public static LocalizationController Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("MySingleton");
                    _instance = singleton.AddComponent<LocalizationController>();
                    DontDestroyOnLoad(singleton);
                }

                return _instance;
            }
        }
        
        public static Action<TypeOfLocalization> onLanguageChanged;
        public static TypeOfLocalization currentLocalization;
        private readonly TypeOfLocalization defaultLocalization = TypeOfLocalization.Ukrainian;

        private void Awake()
        {
            MakeSingleton();
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

        private void MakeSingleton()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}