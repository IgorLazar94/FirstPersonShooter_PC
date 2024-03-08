using System;
using MenuScene;
using Player;
using TMPro;
using UnityEngine;

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
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                actualNoteContent = noteContentEN;
                actualMessage = interectableMessageEN;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                actualNoteContent = noteContentUA;
                actualMessage = interectableMessageUA;
            }
        }
    }
}