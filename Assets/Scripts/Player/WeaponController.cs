using UnityEngine;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Animator pistolAnimator;
        [SerializeField] private ParticleSystem muzzleFlashPistol;

        public void Shot()
        {
            pistolAnimator.SetTrigger(StringAnimCollection.Shot);
            muzzleFlashPistol.Play();
            CheckTarget();
        }

        public void Reload()
        {
            pistolAnimator.SetTrigger(StringAnimCollection.Reload);
        }

        public void SwitchRunState(bool isRun)
        {
            pistolAnimator.SetBool(StringAnimCollection.isRun, isRun);
        }

        private void CheckTarget()
        {
            var transform1 = transform;
            Vector3 raycastOrigin = transform1.position;
            Vector3 raycastDirection = transform1.forward;
            float raycastDistance = 100f;
            if (Physics.Raycast(raycastOrigin, raycastDirection, out var hit, raycastDistance))
            {
                Enemy.Zombie.ZombieBehaviour zombieBehaviour = hit.collider.GetComponent<Enemy.Zombie.ZombieBehaviour>();
                if (zombieBehaviour != null)
                {
                    zombieBehaviour.TakeDamage();
                }
            }
        }
    }
}