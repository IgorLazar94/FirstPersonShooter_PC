using System.Collections;
using Enemy.Zombie;
using SFX;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private DynamicCanvasController dynamicCanvas; // Inject
        [SerializeField] private Animator pistolAnimator;
        [SerializeField] private ParticleSystem muzzleFlashPistol;
        [SerializeField] private FirstPersonController firstPersonController;
        private int bulletsInInventory = 14;
        private int bulletsLoadedInPistol;
        private int maxBulletsInPistol = 7;
        private int pistolDamage = 1;
        private RaycastHitParticlesController raycastHitParticlesController;
        private bool isReadyToShot = true;
        private float timeBetweenShots = 0.35f;
        private float timeToReload = 1f;
        private float timeToMeleeAttack = 1.5f;
        private int ignoreLayerMask;
        private string layerName = "TriggerZoneLayer";

        private void Start()
        {
            ignoreLayerMask = ~LayerMask.GetMask(layerName);
            bulletsLoadedInPistol = maxBulletsInPistol;
            raycastHitParticlesController = GetComponent<RaycastHitParticlesController>();
            dynamicCanvas.UpdateBullets(bulletsLoadedInPistol, bulletsInInventory);
        }

        public void Shot()
        {
            if (!isReadyToShot) return;
            if (bulletsLoadedInPistol <= 0)
            {
                Reload();
                return;
            }

            dynamicCanvas.PlayCrosshairAnim();
            pistolAnimator.SetTrigger(StringAnimCollection.Shot);
            StartCoroutine((DelayBeforeNextShot(timeBetweenShots)));
            muzzleFlashPistol.Play();
            PlayerAudioManager.instance.PlaySFX(AudioCollection.PistolShot);
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
            if (!isReadyToShot) return;
            int bulletsToLoad = Mathf.Min(bulletsInInventory, maxBulletsInPistol - bulletsLoadedInPistol);
            if (bulletsToLoad > 0 && bulletsLoadedInPistol < 7)
            {
                bulletsInInventory -= bulletsToLoad;
                bulletsLoadedInPistol += bulletsToLoad;
                PlayerAudioManager.instance.PlaySFX(AudioCollection.PistolReload);
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
            const float raycastDistance = 100f;
            if (Physics.Raycast(raycastOrigin, raycastDirection, out var hit, raycastDistance, ignoreLayerMask))
            {
                var zombieHead = hit.collider.GetComponent<ZombieHead>();
                var zombieBehaviour = hit.collider.GetComponent<ZombieBehaviour>();

                if (zombieHead != null)
                {
                    dynamicCanvas.PlayDetectAnim();
                    zombieHead.ZombieHeadTakeDamage(pistolDamage);
                }

                if (zombieBehaviour != null)
                {
                    dynamicCanvas.PlayDetectAnim();
                    zombieBehaviour.TakeDamage(pistolDamage);
                }
            }
        }

        public void AddBulletsToInventory(int newBulletsCount)
        {
            bulletsInInventory += newBulletsCount;
            dynamicCanvas.UpdateBullets(bulletsLoadedInPistol, bulletsInInventory);
            PlayerAudioManager.instance.PlaySFX(AudioCollection.PickupAmmoBox);
            if (bulletsLoadedInPistol <= 0)
            {
                Reload();
            }
        }

        public void MeleeAttack()
        {
            if (isReadyToShot && firstPersonController.sprintRemaining > 3f)
            {
                firstPersonController.sprintRemaining -= 3f;
                StartCoroutine(DelayBeforeNextShot(timeToMeleeAttack));
                pistolAnimator.SetTrigger(StringAnimCollection.MeleeAttack);
                Invoke(nameof(CheckNearbyZombies), timeToMeleeAttack / 2);
            }
        }

        public void CheckNearbyZombies()
        {
            firstPersonController.CheckSprintBarVisibility();
            PlayerAudioManager.instance.PlaySFX(AudioCollection.MeleeAttack);
            float radius = 1f;
            Collider[] results = new Collider[10];
            int count = Physics.OverlapSphereNonAlloc(transform.position, radius, results);
            for (int i = 0; i < count; i++)
            {
                var zombieBehaviour = results[i].GetComponent<Enemy.Zombie.ZombieBehaviour>();
                if (zombieBehaviour != null)
                {
                    zombieBehaviour.TakeDamage(0);
                }
            }
        }
    }
}