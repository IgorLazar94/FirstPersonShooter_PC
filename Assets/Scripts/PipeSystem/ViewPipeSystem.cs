using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using MenuScene;

namespace PipeSystem
{
    public class ViewPipeSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshPro titleText;
        [SerializeField] private TextMeshPro contentText;
        private string titleTextEnBad = "Alarm";
        private string titleTextEnGood = "OK";
        private string titleTextUaBad = "Тривога";
        private string titleTextUaGood = "OK";
        private string titleTextActualGood;
        private string titleTextActualBad;
        private string contentTextEn = "Valves closed: ";
        private string contentTextUa = "Клапанів перекрито: ";
        private string contentTextActual;
        private AudioSource alarmAudioSource;
        private Light alarmLight;
        private Tween lightTween;

        private void Awake()
        {
            CheckLocalization();
            alarmLight = GetComponentInChildren<Light>();
            alarmAudioSource = GetComponentInChildren<AudioSource>();
        }

        public void EnableAlarm(bool isActivate)
        {
            if (isActivate)
            {
                alarmLight.enabled = true;
                alarmAudioSource.Play();
                RotateAlarmLight(true);
            }
            else
            {
                RotateAlarmLight(false);
                alarmAudioSource.Stop();
            }
        }

        private void RotateAlarmLight(bool isRotate)
        {
            if (isRotate)
            {
                alarmLight.color = Color.red;
                lightTween = alarmLight.transform.DORotate(new Vector3(360, 0, 0), 360 / 5f, RotateMode.LocalAxisAdd)
                    .SetLoops(-1, LoopType.Incremental)
                    .SetEase(Ease.Linear);
            }
            else
            {
                lightTween.Kill();
                var newRotation = alarmLight.transform.eulerAngles;
                newRotation.x = 90f;
                alarmLight.transform.eulerAngles = newRotation;
                alarmLight.color = Color.green;
            }
        }

        public void UpdateComputerText(bool isAlarm, int closedValves)
        {
            if (isAlarm)
            {
                titleText.color = Color.red;
                titleText.text = titleTextActualBad;
                contentText.color = Color.red;
                contentText.text = contentTextActual + closedValves.ToString();
            }
            else
            {
                titleText.color = Color.green;
                titleText.text = titleTextActualGood;
                contentText.color = Color.green;
                contentText.text = contentTextActual + closedValves.ToString();
            }
        }

        private void CheckLocalization()
        {
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                titleTextActualGood = titleTextEnGood;
                titleTextActualBad = titleTextEnBad;
                contentTextActual = contentTextEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                titleTextActualGood = titleTextUaGood;
                titleTextActualBad = titleTextUaBad;
                contentTextActual = contentTextUa;
            }
        }
    }
}