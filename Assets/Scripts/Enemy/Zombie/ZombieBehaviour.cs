using UnityEngine;

namespace Enemy.Zombie
{
    public class ZombieBehaviour : EnemyUnitBehaviour
    {
        [SerializeField] private float distanceToAttack;
        [SerializeField] private bool isFastZombie;
        private Animator zombieAnimator;
        private bool isAttackPlayer = false;
        private bool isMoveToPlayer;
        private float distanceToPlayer;

        private void Start()
        {
            zombieAnimator = GetComponent<Animator>();
            ChooseRandomIdleType();
        }

        private void Update()
        {
            CheckDistanceToPlayer();
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
            if (!isMoveToPlayer && distanceToPlayer < distanceToAttack)
            {
                isMoveToPlayer = true;
                zombieAnimator.SetBool(StringAnimCollection.isMove, true);
                if (isFastZombie)
                {
                    SetMoveAnimation(0);
                }
                else
                {
                    int random = Random.Range(1, 3);
                    SetMoveAnimation(random);
                }
            }
        }

        private void SetMoveAnimation(int moveType)
        {
            zombieAnimator.SetInteger(StringAnimCollection.typeOfMove, moveType);
        }
    }
}
