using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PipeSystem
{
    public class SteamPipeSystem : MonoBehaviour
    {
        private AudioSource pipeAudioSource;
        private ParticleSystem[] steamParticles;
        private int minRandomValue = 30;
        private int maxRandomValue = 80;

        private void Start()
        {
            pipeAudioSource = GetComponent<AudioSource>();
            steamParticles = GetComponentsInChildren<ParticleSystem>();
            StartCoroutine(SpawnSteamInterval());
        }

        private IEnumerator SpawnSteamInterval()
        {
            while (true)
            {
                int random = Random.Range(minRandomValue, maxRandomValue);
                SteamRelease();
                yield return new WaitForSeconds(random);
            }
        }

        private void SteamRelease()
        {
            pipeAudioSource.Play();
            foreach (var fx in steamParticles)
            {
                fx.Play();
            }
        }
    }
}
