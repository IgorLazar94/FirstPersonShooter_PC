using System;
using UnityEngine;
using MenuScene;

namespace Interactable
{
    public class ExitDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private DynamicCanvasController dynamicCanvas;
        private readonly string interactionMessageEn = "press E to escape from the hospital";
        private readonly string interactionMessageUa = "натисніть Е, щоб втекти зі шпиталю";
        private string actualMessage;

        private void Awake()
        {
            CheckLocalization();
        }

        public string GetInteractionPlayerMessage()
        {
            return actualMessage;
        }

        public void ActivateAction()
        {
            PlayCreditsText();
        }

        public void CheckLocalization()
        {
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                actualMessage = interactionMessageEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                actualMessage = interactionMessageUa;
            }
        }

        private void PlayCreditsText()
        {
            dynamicCanvas.ShowCreditText();
        }
    }
}
