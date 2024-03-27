using Interactable;
using ObjectPool;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(WeaponController))]
    public class RaycastHitParticlesController : MonoBehaviour
    {
        [SerializeField] private BloodFxPool bloodPool;
        [SerializeField] private BulletHolePool bulletHolePool;
        [SerializeField] private SparksPool sparksPool;
        [SerializeField] private SlimeObjectPool slimePool;
        private float distance = 20f;
        private Camera playerCamera;
        private int ignoreLayerMask;
        private string layerName = "TriggerZoneLayer";

        private void Start()
        {
            ignoreLayerMask = ~LayerMask.GetMask(layerName);
            playerCamera = Camera.main;
        }

        private void CreateBulletFx(RaycastHit hit)
        {
            var bulletHole = bulletHolePool.CreateBullet();
            bulletHole.transform.position = hit.point +
                                            new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f,
                                                hit.normal.z * 0.01f);
            bulletHole.transform.rotation = Quaternion.LookRotation(-hit.normal);
        }

        private void CreateSlimeFx(RaycastHit hit)
        {
            var slimesFx = slimePool.CreateSlime();
            slimesFx.transform.position = hit.point +
                                            new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f,
                                                hit.normal.z * 0.01f);
            slimesFx.transform.rotation = Quaternion.LookRotation(-hit.normal);
        }

        private void CreateSparksFx(RaycastHit hit)
        {
            var sparksFx = sparksPool.CreateSparks();
            sparksFx.transform.position = hit.point +
                                          new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f,
                                              hit.normal.z * 0.01f);
            sparksFx.transform.rotation = Quaternion.LookRotation(hit.normal);
        }

        public void CheckTargetPoint()
        {
        
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out var hit, distance, ignoreLayerMask))
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if (hit.collider.gameObject.TryGetComponent(out Enemy.Zombie.ZombieBehaviour zombie)
                    || hit.collider.gameObject.TryGetComponent(out Enemy.Zombie.ZombieHead zombieHead))
                {
                    bloodPool.InitNewBlood(hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f));
                    return;
                }
                if (hit.collider.gameObject.TryGetComponent(out Enemy.Spider.Spider spider))
                {
                    CreateSlimeFx(hit);
                    return;
                }
                if (interactable != null || hit.collider.gameObject.TryGetComponent(out RedBarrel barrel))
                {
                    CreateSparksFx(hit);
                    return;
                }

                CreateBulletFx(hit);
            } 
        }
    }
}
