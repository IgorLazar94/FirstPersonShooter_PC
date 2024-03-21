using System;
using UnityEngine;

namespace Enemy.Zombie
{
    public class ZombieHead : MonoBehaviour
    {
        [SerializeField] private ZombieBehaviour zombieBehaviour;

        private void Start()
        {
            zombieBehaviour.SetZombieHead(this);
        }

        public void ZombieHeadTakeDamage(int damage)
        {
            zombieBehaviour.TakeDamage(damage * 2);
        }
    }
}
