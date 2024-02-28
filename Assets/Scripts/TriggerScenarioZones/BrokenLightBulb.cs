using System;
using Interactable.Lightning;
using UnityEngine;

namespace TriggerScenarioZones
{
    [RequireComponent(typeof(BoxCollider))]
    public class BrokenLightBulb : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ElectricShield electricShield;
        private ParticleSystem sparks;
        private BoxCollider triggerCollider;

        private void Start()
        {
            triggerCollider = GetComponent<BoxCollider>();
            sparks = GetComponentInChildren<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerState player))
            {
                sparks.Play();
                Invoke(nameof(DisableLight), 1.0f);
            }
        }

        private void DisableLight()
        {
            electricShield.DisableElectricity();
            gameManager.SetNewScenarioStage(GameScenarioLevel.BasementWithoutElectricity);
            triggerCollider.enabled = false;
        }
    }
}
