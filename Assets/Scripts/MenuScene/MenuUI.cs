using System;
using UnityEngine;

namespace MenuScene
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject controlPanel;
        private ControlText[] controlTexts;

        private void Start()
        {
            controlTexts = controlPanel.GetComponentsInChildren<ControlText>();
        }

        public void ActivateControlPanel(bool isActive)
        {
            controlPanel.SetActive(isActive);
            CheckLocalization();
        }

        private void CheckLocalization()
        {
            foreach (var controlText in controlTexts)
            {
                controlText.CheckLanguage();
            }
        }
    }
}
