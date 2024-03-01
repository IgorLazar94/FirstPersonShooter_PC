using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using DG.Tweening;
using Player;

namespace Enemy.Zombie
{
    public class ZombieBehaviour : EnemyUnitBehaviour
    {
        [SerializeField] private bool isFastZombie;
        [SerializeField] private int healthPoints;
        [SerializeField] private int remainingResurrection;
        [SerializeField] private AudioClip zombieAttack, zombieAttack2, takeDamage, takeDamage2, findPlayer, findPlayer2, zombieDie, zombieDie2;
        private AudioSource audioSource;
        private CapsuleCollider zombieCollider;
        private int defaultHealthPoints;
        private float distanceToAttack = 2.5f;
        private Animator zombieAnimator;
        private NavMeshAgent zombieAgent;
        private bool isAttackPlayer;
        private bool isMoveToPlayer;
        private float distanceToPlayer;
        private float distanceMoveToPlayer = 10f;
        private bool isDeath;
        private PlayerState playerState;
        private int zombieDamage = 10;
        private float timeSinceLastAttack = 0f;
        private float attackInterval = 1.5f;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            zombieCollider = GetComponent<CapsuleCollider>();
            playerState = player.GetComponent<PlayerState>();
            defaultHealthPoints = healthPoints;
            zombieAnimator = GetComponent<Animator>();
            zombieAgent = GetComponent<NavMeshAgent>();
            ChooseRandomIdleType();
            StartCoroutine(StartCheckDistanceToPlayer());
        }

        private void Update()
        {
            distanceToPlayer = (transform.position - player.transform.position).magnitude;
        }

        private void FixedUpdate()
        {
            if (isMoveToPlayer && !isDeath)
            {
                if (distanceToPlayer < distanceToAttack)
                {
                    AttackPlayer();
                    isAttackPlayer = true;
                }
                else
                {
                    zombieAgent.SetDestination(player.transform.position);
                    zombieAnimator.SetBool(StringAnimCollection.isAttack, false);
                    isAttackPlayer = false;
                }
            }
        }

        private void AttackPlayer()
        {
            timeSinceLastAttack += Time.fixedDeltaTime;
            if (timeSinceLastAttack >= attackInterval)
            {
                timeSinceLastAttack = 0f;
                zombieAnimator.SetBool(StringAnimCollection.isAttack, true);
                int random = Random.Range(0, 3);
                zombieAnimator.SetInteger(StringAnimCollection.typeOfAttack, random);
                playerState.PlayerTakeDamage(zombieDamage);
                PlayRandomAudio(zombieAttack, zombieAttack2);
            }
        }

        private void PlayRandomAudio(AudioClip option1, AudioClip option2)
        {
            int random = Random.Range(0, 2);
            if (random > 0)
            {
                audioSource.clip = option1;
            }
            else
            {
                audioSource.clip = option2;
            }
            audioSource.Play();
        }

        private void ChooseRandomIdleType()
        {
            int random = Random.Range(0, 3);
            zombieAnimator.SetBool(StringAnimCollection.isIdle, true);
            zombieAnimator.SetInteger(StringAnimCollection.typeOfIdle, random);
        }

        private IEnumerator StartCheckDistanceToPlayer()
        {
            while (!isMoveToPlayer)
            {
                yield return new WaitForSeconds(0.75f);
                CheckDistanceToPlayer();
            }
        }

        private void CheckDistanceToPlayer()
        {
            if (!isMoveToPlayer && distanceToPlayer < distanceMoveToPlayer && !isDeath)
            {
                PlayRandomAudio(findPlayer, findPlayer2);
                isMoveToPlayer = true;
                zombieAnimator.SetBool(StringAnimCollection.isMove, true);
                if (isFastZombie)
                {
                    SetMoveAnimation(0);
                    zombieAgent.SetDestination(player.transform.position);
                    zombieAgent.speed *= 1.5f;
                }
                else
                {
                    int random = Random.Range(1, 3);
                    SetMoveAnimation(random);
                    zombieAgent.SetDestination(player.transform.position);
                }
            }
        }

        private void SetMoveAnimation(int moveType)
        {
            zombieAnimator.SetBool(StringAnimCollection.isMove, true);
            zombieAnimator.SetInteger(StringAnimCollection.typeOfMove, moveType);
        }

        public void TakeDamage(int damage)
        {
            if (isDeath) return;
            healthPoints -= damage;
            PlayRandomAudio(takeDamage, takeDamage2);
            if (healthPoints <= 0)
            {
                ChanceToAnimatedDamage(true);
            }
            else
            {
                ChanceToAnimatedDamage(false);
            }

            zombieAgent.SetDestination(player.transform.position);
            isMoveToPlayer = true;
            int random = Random.Range(1, 3);
            SetMoveAnimation(random);
            CheckHp();
        }

        private void ChanceToAnimatedDamage(bool isNecessarily)
        {
            if (isNecessarily)
            {
                zombieAnimator.SetTrigger(StringAnimCollection.takeDamage);
            }
            else
            {
                int random = Random.Range(0, 5);
                if (random == 0)
                {
                    zombieAnimator.SetTrigger(StringAnimCollection.takeDamage);
                }
            }
        }

        private void CheckHp()
        {
            if (healthPoints <= 0)
            {
                int random = Random.Range(0, 2);
                zombieAnimator.SetBool(StringAnimCollection.isDeath, true);
                zombieAnimator.SetInteger(StringAnimCollection.typeOfDeath, random);
                zombieAgent.isStopped = true;
                zombieAgent.ResetPath();
                isDeath = true;
                SwitchZombieCollider(isDeath);
                PlayRandomAudio(zombieDie, zombieDie2);
                if (remainingResurrection > 0)
                {
                    remainingResurrection--;
                    StartCoroutine(ZombieResurrection());
                }
            }
        }

        private IEnumerator ZombieResurrection()
        {
            yield return new WaitForSeconds(10f);
            zombieAnimator.SetTrigger(StringAnimCollection.resurrection);
            zombieAnimator.SetBool(StringAnimCollection.isDeath, false);
            isDeath = false;
            SwitchZombieCollider(isDeath);
            healthPoints = defaultHealthPoints;
        }

        private void SwitchZombieCollider(bool isZombieDeath)
        {
            zombieCollider.enabled = !isZombieDeath;
            if (!isZombieDeath)
            {
                const float targetRadius = 0.3f;
                const float duration = 0.5f;
                zombieCollider.radius = 0f;
                DOTween.Kill(zombieCollider);
                var initialRadius = zombieCollider.radius;
                float[] radiusArray = {initialRadius, targetRadius};
                DOTween.To(() => radiusArray[0], x => radiusArray[0] = x, targetRadius, duration)
                    .OnUpdate(() => zombieCollider.radius = radiusArray[0]);
            }
        }
    }
}