using System;
using Interactable;
using UnityEngine;
using DG.Tweening;

namespace PipeSystem
{
    public class Valve : MonoBehaviour, IInteractable
    {
        private PipeSystem pipeSystem;
        private string message = "press E to close valve";
        private bool isOpen = true;
        
        public void SetPipeSystem(PipeSystem pipeSystem)
        {
            this.pipeSystem = pipeSystem;
        }

        public string GetInteractionPlayerMessage()
        {
            return isOpen ? message : null;
        }

        public void ActivateAction()
        {
            if (!isOpen) return;
            isOpen = false;
            pipeSystem.CloseValve();
            CloseValveAnimation();
        }

        private void CloseValveAnimation()
        {
            transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 1.0f, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear);
        }
    }
}
