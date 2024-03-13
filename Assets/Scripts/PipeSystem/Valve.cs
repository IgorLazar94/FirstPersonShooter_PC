using System;
using Interactable;
using UnityEngine;
using DG.Tweening;
using MenuScene;

namespace PipeSystem
{
    public class Valve : MonoBehaviour, IInteractable
    {
        private SingleValveLight singleValveLight;
        private AudioSource audioSource;
        private PipeSystem pipeSystem;
        private readonly string messageEn = "press E to close valve";
        private readonly string messageUa = "press E to close valve";
        private string actualMessage;
        private bool isOpen = true;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            singleValveLight = transform.parent.GetComponentInChildren<SingleValveLight>();
            singleValveLight.SwitchLight(isOpen);
            CheckLocalization();
        }

        public void SetPipeSystem(PipeSystem pipeSystem)
        {
            this.pipeSystem = pipeSystem;
        }

        public string GetInteractionPlayerMessage()
        {
            return isOpen ? actualMessage : null;
        }

        public void ActivateAction()
        {
            if (!isOpen) return;
            audioSource.Play();
            isOpen = false;
            pipeSystem.CloseValve();
            CloseValveAnimation();
        }

        public void CheckLocalization()
        {
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                actualMessage = messageEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                actualMessage = messageUa;
            }
        }

        private void CloseValveAnimation()
        {
            transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 1.0f, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear).OnComplete(() => singleValveLight.SwitchLight(isOpen));
        }
    }
}
