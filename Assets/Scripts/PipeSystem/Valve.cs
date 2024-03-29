using Interactable;
using UnityEngine;
using DG.Tweening;
using MenuScene;
using Zenject;

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
        private LocalizationController localizationController;
        
        [Inject]
        private void Construct(LocalizationController localizationController)
        {
            this.localizationController = localizationController;
        }

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
            if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
            {
                actualMessage = messageEn;
            }
            else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
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
