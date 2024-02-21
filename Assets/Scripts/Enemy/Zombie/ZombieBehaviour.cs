using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

namespace Enemy.Zombie
{
    public class ZombieBehaviour : EnemyUnitBehaviour
    {
        [SerializeField] private bool isFastZombie;
        [SerializeField] private int healthPoints;
        [SerializeField] private int remainingResurrection;
        private int defaultHealthPoints;
        private float distanceToAttack = 2f;
        private Animator zombieAnimator;
        private NavMeshAgent zombieAgent;
        private bool isAttackPlayer;
        private bool isMoveToPlayer;
        private float distanceToPlayer;
        private float distanceMoveToPlayer = 10f;
        [SerializeField] private bool isDeath;
        private PlayerState playerState;
        private int zombieDamage = 10;
        private float timeSinceLastAttack = 0f;
        private float attackInterval = 1.5f;

        private void Start()
        {
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
            }
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
                yield return new WaitForSeconds(2f);
                CheckDistanceToPlayer();
            }
        }

        private void CheckDistanceToPlayer()
        {
            if (!isMoveToPlayer && distanceToPlayer < distanceMoveToPlayer && !isDeath)
            {
                isMoveToPlayer = true;
                zombieAnimator.SetBool(StringAnimCollection.isMove, true);
                if (isFastZombie)
                {
                    SetMoveAnimation(0);
                    zombieAgent.SetDestination(player.transform.position);
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
            Debug.Log("is death false");
            healthPoints = defaultHealthPoints;
        }
    }
}