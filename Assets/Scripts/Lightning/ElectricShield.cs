using DG.Tweening;
using UnityEngine;

namespace Lightning
{
    public class ElectricShield : MonoBehaviour, IInteractable
    {
        [SerializeField] private string message = "Press E to enable electric shield";
        [SerializeField] private Transform alarmLight;
        private Animator shieldAnimator;
        private Tween alarmLightTween;

        private void Start()
        {
            shieldAnimator = GetComponent<Animator>();
            RotateAlarmLight();
        }

        public string GetInteractionPlayerMessage()
        {
            return message;
        }

        public void ActivateAction()
        {
            shieldAnimator.SetTrigger(StringAnimCollection.OpenShield);
        }

        public void OnHandleLevelArmUp() // Animation Event
        {
            alarmLightTween.Kill();
            alarmLight.GetComponent<Light>().enabled = false;
            Debug.Log("Enable light");
        }

        private void RotateAlarmLight()
        {
            alarmLightTween = alarmLight.DORotate(new Vector3(360f, 0f, 0f), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .OnComplete(RotateAlarmLight); 
        }
    }
}
