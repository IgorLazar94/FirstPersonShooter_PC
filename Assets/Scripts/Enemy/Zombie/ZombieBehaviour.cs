using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : EnemyUnitBehaviour
{
    [SerializeField] private float distanceToAttack;
    [SerializeField] private bool isFastZombie;
    private Animator zombieAnimator;
    private bool isAttackPlayer = false;
    private bool isMoveToPlayer = false;
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
        zombieAnimator.SetBool("isIdle", true);
        zombieAnimator.SetInteger("typeOfIdle", random);
    }

    private void CheckDistanceToPlayer()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (!isMoveToPlayer && distanceToPlayer < distanceToAttack)
        {
            isMoveToPlayer = true;
            zombieAnimator.SetBool("isMove", true);
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
        Debug.Log("activate type of move" + moveType);
        zombieAnimator.SetInteger("typeOfMove", moveType);
    }
}
