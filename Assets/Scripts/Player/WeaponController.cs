using System.Collections;
using UnityEngine;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private DynamicCanvasController dynamicCanvas; // Inject
        [SerializeField] private Animator pistolAnimator;
        [SerializeField] private ParticleSystem muzzleFlashPistol;
        private int bulletsInInventory = 14;
        private int bulletsLoadedInPistol;
        private int maxBulletsInPistol = 7;
        private int pistolDamage = 1;
        private RaycastHitParticlesController raycastHitParticlesController;
        private bool isReadyToShot = true;
        private float timeBetweenShots = 0.35f;
        private float timeToReload = 1f;
        private void Start()
        {
            bulletsLoadedInPistol = maxBulletsInPistol;
            raycastHitParticlesController = GetComponent<RaycastHitParticlesController>();
            dynamicCanvas.UpdateBullets(bulletsLoadedInPistol, bulletsInInventory);
        }

        public void Shot()
        {
            if (!isReadyToShot) return;
            if (bulletsLoadedInPistol <= 0) return;
            pistolAnimator.SetTrigger(StringAnimCollection.Shot);
            StartCoroutine((DelayBeforeNextShot(timeBetweenShots)));
            muzzleFlashPistol.Play();
            bulletsLoadedInPistol--;
            dynamicCanvas.UpdateBullets(bulletsLoadedInPistol, bulletsInInventory);
            CheckTarget();
            raycastHitParticlesController.CheckTargetPoint();
        }
        
        private IEnumerator DelayBeforeNextShot(float time)
        {
            isReadyToShot = false;
            yield return new WaitForSeconds(time);
            isReadyToShot = true;
        }

        public void Reload()
        {
            int bulletsToLoad = Mathf.Min(bulletsInInventory, maxBulletsInPistol - bulletsLoadedInPistol);
            if (bulletsToLoad > 0 && bulletsLoadedInPistol < 7)
            {
                bulletsInInventory -= bulletsToLoad;
                bulletsLoadedInPistol += bulletsToLoad;
                StartCoroutine(DelayBeforeNextShot(timeToReload));
            }
            else
            {
                return;
            }
            dynamicCanvas.UpdateBullets(bulletsLoadedInPistol, bulletsInInventory);
            pistolAnimator.SetTrigger(StringAnimCollection.Reload);
        }

        public void SwitchRunState(bool isRun)
        {
            pistolAnimator.SetBool(StringAnimCollection.isRun, isRun);
            isReadyToShot = !isRun;
        }

        private void CheckTarget()
        {
            var transform1 = transform;
            var raycastOrigin = transform1.position;
            var raycastDirection = transform1.forward;
            var raycastDistance = 100f;
            if (Physics.Raycast(raycastOrigin, raycastDirection, out var hit, raycastDistance))
            {
                var zombieBehaviour = hit.collider.GetComponent<Enemy.Zombie.ZombieBehaviour>();
                if (zombieBehaviour != null)
                {
                    zombieBehaviour.TakeDamage(pistolDamage);
                }
            }
        }

        public void AddBulletsToInventory(int newBulletsCount)
        {
            bulletsInInventory += newBulletsCount;
            dynamicCanvas.UpdateBullets(bulletsLoadedInPistol, bulletsInInventory);
        }
    }
}