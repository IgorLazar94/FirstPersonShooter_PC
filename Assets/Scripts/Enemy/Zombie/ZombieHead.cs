using System;
using UnityEngine;

namespace Enemy.Zombie
{
    public class ZombieHead : MonoBehaviour
    {
        [SerializeField] private ZombieBehaviour zombieBehaviour;
        private const int DamageMultiplier = 2;

        private void Start()
        {
            zombieBehaviour.SetZombieHead(this);
        }

        public void ZombieHeadTakeDamage(int damage)
        {
            zombieBehaviour.TakeDamage(damage * DamageMultiplier);
        }
    }
}
