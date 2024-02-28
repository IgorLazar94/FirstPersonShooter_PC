using System;
using DG.Tweening;
using UnityEngine;

namespace Interactable.Lightning
{
    public class ElectricShield : MonoBehaviour, IInteractable
    {
        public static event OnSwitchElectricity switchElectricity;
        public delegate void OnSwitchElectricity(bool isEnable);
        // public static event Action<bool> OnSwitchElectricity;

        public static bool isHasElectricity;

        [SerializeField] private string message = "Press E to enable electric shield";

        [SerializeField] private Transform alarmLight;

        [SerializeField] private Light[] connectLights;

        private Animator shieldAnimator;

        private Tween alarmLightTween;

        private void Start()
        {
            isHasElectricity = true;
            shieldAnimator = GetComponent<Animator>();
            alarmLight.gameObject.SetActive(false);
        }

        public string GetInteractionPlayerMessage()
        {
            if (!isHasElectricity)
            {
                return message;
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

        public void OnHandleLevelArmUp() // Animation Event
        {
            alarmLightTween.Kill();
            alarmLight.GetComponent<Light>().enabled = false;
            isHasElectricity = true;
            ActivateConnectLights(true);
            switchElectricity?.Invoke(true);
        }

        private void RotateAlarmLight()
        {
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
            switchElectricity?.Invoke(false);
        }

        private void ActivateConnectLights(bool isActive)
        {
            foreach (var light1 in connectLights)
            {
                light1.enabled = isActive;
            }
        }
    }
}