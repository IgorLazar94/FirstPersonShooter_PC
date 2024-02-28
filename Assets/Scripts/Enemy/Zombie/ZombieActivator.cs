using UnityEngine;

namespace Enemy.Zombie
{
    public class ZombieActivator : MonoBehaviour
    {
        [SerializeField] private Transform[] zombieGroups;
        private int activateZombieGroupsCounter;

        public void ActivateNewGroupOfZombies()
        {
            if (zombieGroups[activateZombieGroupsCounter] == null) return;
            zombieGroups[activateZombieGroupsCounter].gameObject.SetActive(true);
            activateZombieGroupsCounter++;
        }
    }
}