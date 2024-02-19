using ObjectPool;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy.Zombie
{
    public class ZombieBehaviour : EnemyUnitBehaviour
    {
        [SerializeField] private bool isFastZombie;
        private float distanceToAttack = 2f;
        private Animator zombieAnimator;
        private NavMeshAgent zombieAgent;
        private bool isAttackPlayer;
        private bool isMoveToPlayer;
        private float distanceToPlayer;
        private float distanceMoveToPlayer = 10f;

        private void Start()
        {
            zombieAnimator = GetComponent<Animator>();
            zombieAgent = GetComponent<NavMeshAgent>();
            ChooseRandomIdleType();
        }

        private void Update()
        {
            CheckDistanceToPlayer();
        }

        private void FixedUpdate()
        {
            if (isMoveToPlayer)
            {
                if (distanceToPlayer < distanceToAttack)
                {
                    AttackPlayer();
                    isAttackPlayer = true;
                    CheckDistanceToPlayer();
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
            zombieAnimator.SetBool(StringAnimCollection.isAttack, true);
            int random = Random.Range(0, 3);
            zombieAnimator.SetInteger(StringAnimCollection.typeOfAttack, random);
        }

        private void ChooseRandomIdleType()
        {
            int random = Random.Range(0, 3);
            zombieAnimator.SetBool(StringAnimCollection.isIdle, true);
            zombieAnimator.SetInteger(StringAnimCollection.typeOfIdle, random);
        }

        private void CheckDistanceToPlayer()
        {
            distanceToPlayer = (transform.position - player.transform.position).magnitude;
            if (!isMoveToPlayer && distanceToPlayer < distanceMoveToPlayer)
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
            zombieAnimator.SetInteger(StringAnimCollection.typeOfMove, moveType);
        }

        public void TakeDamage()
        {
            ChanceToAnimatedDamage();
            zombieAgent.SetDestination(player.transform.position);
            isMoveToPlayer = true;
        }

        private void ChanceToAnimatedDamage()
        {
            int random = Random.Range(0, 5);
            if (random == 0)
            {
                zombieAnimator.SetTrigger(StringAnimCollection.takeDamage);
            }
        }
    }
}