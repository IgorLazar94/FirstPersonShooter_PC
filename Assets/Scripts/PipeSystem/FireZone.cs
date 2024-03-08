using System;
using Player;
using UnityEngine;

namespace PipeSystem
{
    public class FireZone : MonoBehaviour
    {
        private readonly int damageFromFire = 15;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerState player))
            {
                player.PlayerTakeDamage(damageFromFire);
            }
        }

        public void PlayFireSound()
        {
            audioSource.Play();
        }
    }
}
