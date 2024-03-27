using Enemy.Spider;
using Player;
using UnityEngine;

namespace TriggerScenarioZones
{
    public class SpiderZone : MonoBehaviour
    {
        private SpiderSpawner spiderSpawner;
        private BoxCollider triggerZoneCollider;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            triggerZoneCollider = GetComponent<BoxCollider>();
            spiderSpawner = GetComponentInChildren<SpiderSpawner>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerState player))
            {
                triggerZoneCollider.enabled = false;
                audioSource.Play();
                spiderSpawner.StartSpawnSpiders();
            }
        }
    }
}
