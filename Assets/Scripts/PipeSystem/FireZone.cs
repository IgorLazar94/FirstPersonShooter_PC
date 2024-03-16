using System.Collections;
using Player;
using UnityEngine;

namespace PipeSystem
{
    public class FireZone : MonoBehaviour
    {
        private readonly int damageFromFire = 15;
        private AudioSource audioSource;
        private BoxCollider fireCollider;
        private bool playerInRange;

        private void Start()
        {
            playerInRange = false;
            fireCollider = GetComponent<BoxCollider>();
            audioSource = GetComponent<AudioSource>();
        }

        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.gameObject.TryGetComponent(out PlayerState player))
        //     {
        //         player.PlayerTakeDamage(damageFromFire);
        //     }
        // }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerState player))
            {
                playerInRange = true;
                StartCoroutine(DealDamageCoroutine(player));
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Exit");
            if (other.gameObject.TryGetComponent(out PlayerState player))
            {
                playerInRange = false;
                StopCoroutine(DealDamageCoroutine(player));
            }
        }

        
        private IEnumerator DealDamageCoroutine(PlayerState player)
        {
            while (playerInRange)
            {
                player.PlayerTakeDamage(damageFromFire);
                yield return new WaitForSeconds(0.33f);
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
