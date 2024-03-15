using System;
using Player;
using UnityEngine;

namespace PipeSystem
{
    public class FireZone : MonoBehaviour
    {
        private readonly int damageFromFire = 15;
        private AudioSource audioSource;
        private BoxCollider fireCollider;

        private void Start()
        {
            fireCollider = GetComponent<BoxCollider>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerState player))
            {
                player.PlayerTakeDamage(damageFromFire);
            }
        }

        public void PlayFireSound(bool isPlay)
        {
            if (isPlay)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }

        public void DisableFireZoneCollider()
        {
            fireCollider.enabled = false;
        }
    }
}
