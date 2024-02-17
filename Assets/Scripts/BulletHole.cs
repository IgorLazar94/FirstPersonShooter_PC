using UnityEngine;

public class BulletHole : MonoBehaviour
{
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private ParticleSystem sparksFx;
    [SerializeField] private ParticleSystem[] bloodFxArray;
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
                    Instantiate(ChooseRandomBlood(), hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f), Quaternion.LookRotation(hit.normal));
                    return;
                }
                if (interactable != null)
                {
                    Instantiate(sparksFx, hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f), Quaternion.LookRotation(hit.normal));
                    return;
                }

                Instantiate(bulletHolePrefab, hit.point + new Vector3(hit.normal.x * 0.01f, hit.normal.y * 0.01f, hit.normal.z * 0.01f), Quaternion.LookRotation(-hit.normal));
            }
        }
    }

    private ParticleSystem ChooseRandomBlood()
    {
        int random = Random.Range(0, bloodFxArray.Length);
        return bloodFxArray[random];
    }
}
