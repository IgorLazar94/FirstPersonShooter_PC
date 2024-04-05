using System;
using Player;
using UnityEngine;
using Zenject;

namespace TriggerScenarioZones
{
    public class PipeExplosion : MonoBehaviour
    {
        [SerializeField] private PipeSystem.PipeSystem pipeSystem;
        private GameManager gameManager;
        private BoxCollider triggerCollider;
        private AudioSource audioSource;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        
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
