using System;
using DG.Tweening;
using UnityEngine;
using MenuScene;

namespace Interactable.Lightning
{
    public class ElectricShield : MonoBehaviour, IInteractable
    {
        public static event OnSwitchElectricity SwitchElectricity;
        public delegate void OnSwitchElectricity(bool isEnable);

        [SerializeField] private GameManager gameManager;
        [SerializeField] private Transform alarmLight;
        [SerializeField] private Light[] connectLights;
        [SerializeField] private AudioClip lightSfx;
        [SerializeField] private AudioClip alarmSfx;
        private string messageEn = "Press E to enable electric shield";
        private string messageUA = "натисніть Е, щоб взаємодіяти з електрощитком";
        private string actualLanguage;
        private AudioSource audioSource;
        private Animator shieldAnimator;
        private Tween alarmLightTween;
        private bool isHasElectricity;

        private void Start()
        {
            CheckLocalization();
            isHasElectricity = true;
            audioSource = GetComponent<AudioSource>();
            shieldAnimator = GetComponent<Animator>();
            alarmLight.gameObject.SetActive(false);
        }

        public string GetInteractionPlayerMessage()
        {
            if (!isHasElectricity)
            {
                return actualLanguage;
            }
            else
            {
                return null;
            }
        }

        public void ActivateAction()
        {
            if (!isHasElectricity)
            {
                shieldAnimator.SetTrigger(StringAnimCollection.OpenShield);
            }
        }

        public void CheckLocalization()
        {
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                actualLanguage = messageEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                actualLanguage = messageUA;
            }
        }

        public void OnHandleLevelArmUp() // Animation Event
        {
            alarmLightTween.Kill();
            alarmLight.GetComponent<Light>().enabled = false;
            isHasElectricity = true;
            ActivateConnectLights(true);
            SwitchElectricity?.Invoke(true);
            gameManager.ActivateNewZombieGroup();
            QuestSystem.OnUpdateQuest?.Invoke();
        }

        private void RotateAlarmLight()
        {
            audioSource.clip = alarmSfx;
            audioSource.Play();
            alarmLightTween = alarmLight.DORotate(new Vector3(360f, 0f, 0f), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .OnComplete(RotateAlarmLight);
        }

        public void DisableElectricity()
        {
            isHasElectricity = false;
            alarmLight.gameObject.SetActive(true);
            RotateAlarmLight();
            ActivateConnectLights(false);
            SwitchElectricity?.Invoke(false);
        }

        private void ActivateConnectLights(bool isActive)
        {
            foreach (var light1 in connectLights)
            {
                light1.enabled = isActive;
            }

            if (isActive != true) return;
            audioSource.clip = lightSfx;
            audioSource.Play();
        }
    }
}