using UnityEngine;
using Zenject;

public class BulletHolePool : MonoBehaviour
{
    private BulletHoleGameObject playerBulletHolePrefab;
    private int poolCount = 30;
    private bool autoExpand = true;

    private ObjectPool<BulletHoleGameObject> pool;

    [Inject]
    private void Construct(BulletHoleGameObject bulletHolePrefab)
    {
        playerBulletHolePrefab = bulletHolePrefab;
    }

    private void Start()
    {
        this.pool = new ObjectPool<BulletHoleGameObject>(playerBulletHolePrefab, poolCount, this.transform)
        {
            autoExpand = this.autoExpand
        };
    }
 

    public BulletHoleGameObject CreateBullet()
    {
        var bullet = this.pool.GetFreeElement();
        return bullet;
    }
}
