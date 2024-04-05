using Zenject;
using Interactable.Lightning;
using Player;
using UnityEngine;

namespace TriggerScenarioZones
{
    [RequireComponent(typeof(BoxCollider))]
    public class BrokenLightBulb : MonoBehaviour
    {
        [SerializeField] private ElectricShield electricShield;
        [SerializeField] private AudioClip electricityAudioClip;
        [SerializeField] private AudioClip brokenGlassAudioClip;
        private GameManager gameManager;
        private ParticleSystem sparks;
        private BoxCollider triggerCollider;
        private AudioSource sfxSource;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private void Start()
        {
            sfxSource = GetComponentInChildren<AudioSource>();
            triggerCollider = GetComponent<BoxCollider>();
            sparks = GetComponentInChildren<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerState player))
            {
                sfxSource.clip = electricityAudioClip;
                sfxSource.Play();
                sparks.Play();
                Invoke(nameof(DisableLight), 1.0f);
            }
        }

        private void DisableLight()
        {
            sfxSource.clip = brokenGlassAudioClip;
            sfxSource.Play();
            electricShield.DisableElectricity();
            gameManager.SetNewScenarioStage(GameScenarioLevel.BasementWithoutElectricity);
            triggerCollider.enabled = false;
        }
    }
}
