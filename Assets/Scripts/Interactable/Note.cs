using MenuScene;
using TMPro;
using UnityEngine;
using Zenject;

namespace Interactable
{
    public class Note : MonoBehaviour, IInteractable
    {
        [SerializeField] private DynamicCanvasController dynamicCanvas;
        [TextArea(5, 10)] [SerializeField] private string noteContentEN;
        [TextArea(5, 10)] [SerializeField] private string noteContentUA;
        private string actualNoteContent;
        private TextMeshPro note3DText;
        private readonly string interectableMessageEN = "press E to read the note";
        private readonly string interectableMessageUA = "Натисніть Е, щоб прочитати записку";
        private string actualMessage;
        private LocalizationController localizationController;
        
        [Inject]
        private void Construct(LocalizationController localizationController)
        {
            this.localizationController = localizationController;
        }


        private void Start()
        {
            CheckLocalization();
            note3DText = GetComponentInChildren<TextMeshPro>();
            note3DText.text = actualNoteContent;
        }

        public string GetInteractionPlayerMessage()
        {
            return actualMessage;
        }

        public void ActivateAction()
        {
            dynamicCanvas.ShowNote(actualNoteContent);
        }

        public void CheckLocalization()
        {
            if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
            {
                actualNoteContent = noteContentEN;
                actualMessage = interectableMessageEN;
            }
            else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
            {
                actualNoteContent = noteContentUA;
                actualMessage = interectableMessageUA;
            }
        }
    }
}