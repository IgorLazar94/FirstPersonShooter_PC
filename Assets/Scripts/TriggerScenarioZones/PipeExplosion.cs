using System;
using Player;
using UnityEngine;

namespace TriggerScenarioZones
{
    public class PipeExplosion : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PipeSystem.PipeSystem pipeSystem;
        private BoxCollider triggerCollider;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            triggerCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerState player))
            {
                gameManager.SetNewScenarioStage(GameScenarioLevel.BurstPipe);
                pipeSystem.StartFire();
                triggerCollider.enabled = false;
                audioSource.Play();
            }
        }
    }
}
