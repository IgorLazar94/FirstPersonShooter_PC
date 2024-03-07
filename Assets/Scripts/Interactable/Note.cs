using System;
using Player;
using TMPro;
using UnityEngine;

namespace Interactable
{
    public class Note : MonoBehaviour, IInteractable
    {
        [SerializeField] private DynamicCanvasController dynamicCanvas;
        [TextArea (5, 10)] [SerializeField] private string noteContentEN;
        [TextArea (5, 10)] [SerializeField] private string noteContentUA;
        private string actualNoteContent;
        private TextMeshPro note3DText;
        private readonly string interectableMessage = "press E to read the note";

        private void Start()
        {
            note3DText = GetComponentInChildren<TextMeshPro>();
            actualNoteContent = noteContentEN;
            note3DText.text = actualNoteContent;
        }

        public string GetInteractionPlayerMessage()
        {
            return interectableMessage;
        }

        public void ActivateAction()
        {
            dynamicCanvas.ShowNote(actualNoteContent);
        }

    }
}
