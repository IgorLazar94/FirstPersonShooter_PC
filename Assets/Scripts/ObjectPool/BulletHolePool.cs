using UnityEngine;

public class BulletHolePool : MonoBehaviour
{
    [SerializeField] private BulletHoleGameObject playerBulletPrefab;
    private int poolCount = 30;
    private bool autoExpand = true;

    private BulletPool<BulletHoleGameObject> pool;

    private void Start()
    {
        this.pool = new BulletPool<BulletHoleGameObject>(playerBulletPrefab, poolCount, this.transform)
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
