using System;
using ModularFirstPersonController.FirstPersonController;
using Player;
using UnityEngine;

namespace Enemy.Spider
{
    public class Spider : EnemyUnitBehaviour
    {
        private ParticleSystem[] spiderDieParticle;
        private Animator spiderAnimator;
        private AudioSource spiderAudioSource;
        private AudioClip spiderMove, spiderAttack, spiderDie;
        private float speed = 2.5f;
        private float distanceToAttack = 3f;
        private int spiderDamage = 5;

        private void Start()
        {
            spiderAudioSource = GetComponent<AudioSource>();
            spiderDieParticle = GetComponentsInChildren<ParticleSystem>();
            spiderAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            var currentPosition = transform.position;
            var targetPosition = player.transform.position;

            float distanceToPlayer = Vector3.Distance(currentPosition, targetPosition);

            if (distanceToPlayer < distanceToAttack)
            {
                PlaySfx(spiderAttack);
                spiderAnimator.SetTrigger(StringAnimCollection.isAttack);
                var newPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
                transform.position = newPosition;
                
            }
            else
            {
                PlaySfx(spiderMove);
                targetPosition.y = currentPosition.y;
                var newPosition = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
                transform.position = newPosition;
            }
            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerState player))
            {
                player.PlayerTakeDamage(spiderDamage);
                SpiderDie();
            }
        }

        public void SpiderDie()
        {
            PlaySfx(spiderDie);
            foreach (var fx in spiderDieParticle)
            {
                fx.Play();
                fx.transform.parent = null;
            }
            DestroySpider();
        }

        private void DestroySpider()
        {
            Destroy(this.gameObject);
        }

        public void SetPlayer(FirstPersonController _player)
        {
            this.player = _player;
        }

        private void PlaySfx(AudioClip audioClip)
        {
            if (spiderAudioSource.isPlaying && spiderAudioSource.clip == audioClip) return;
            spiderAudioSource.clip = audioClip;
            spiderAudioSource.Play();
        }

        public void SetAudio(AudioClip die, AudioClip attack, AudioClip move)
        {
            spiderDie = die;
            spiderAttack = attack;
            spiderMove = move;
        }
    }
}