using System;
using UnityEngine;
using MenuScene;
using Zenject;

namespace Interactable
{
    public class ExitDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private DynamicCanvasController dynamicCanvas;
        private readonly string interactionMessageEn = "press E to escape from the hospital";
        private readonly string interactionMessageUa = "натисніть Е, щоб втекти зі шпиталю";
        private string actualMessage;
        private LocalizationController localizationController;
        
        [Inject]
        private void Construct(LocalizationController localizationController)
        {
            this.localizationController = localizationController;
        }


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
            if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
            {
                actualMessage = interactionMessageEn;
            }
            else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
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
