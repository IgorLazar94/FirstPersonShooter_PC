using ObjectPool;
using UnityEngine;

public class RaycastHitParticlesController : MonoBehaviour
{
    [SerializeField] private BloodFxPool bloodPool;
    [SerializeField] private BulletHolePool bulletHolePool;
    [SerializeField] private SparksPool sparksPool;
    private float distance = 20f;
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out var hit, distance))
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if (hit.collider.gameObject.TryGetComponent(out Enemy.Zombie.ZombieBehaviour zombie))
                {
                    bloodPool.InitNewBlood(hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f));
                    return;
                }
                if (interactable != null)
                {
                    CreateSparksFx(hit);
                    return;
                }

                CreateBulletFx(hit);
            }
        }
    }

    private void CreateBulletFx(RaycastHit hit)
    {
        var bulletHole = bulletHolePool.CreateBullet();
        bulletHole.transform.position = hit.point +
                                        new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f,
                                            hit.normal.z * 0.01f);
        bulletHole.transform.rotation = Quaternion.LookRotation(-hit.normal);
    }

    private void CreateSparksFx(RaycastHit hit)
    {
        var sparksFx = sparksPool.CreateSparks();
        sparksFx.transform.position = hit.point +
                                      new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f,
                                          hit.normal.z * 0.01f);
        sparksFx.transform.rotation = Quaternion.LookRotation(hit.normal);
    }

}
