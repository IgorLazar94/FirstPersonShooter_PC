using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeObjectPool : MonoBehaviour
{
    [SerializeField] private SlimeGameObject slimePrefab;
    private int poolCount = 7;
    private bool autoExpand = true;

    private BulletPool<SlimeGameObject> pool;

    private void Start()
    {
        this.pool = new BulletPool<SlimeGameObject>(slimePrefab, poolCount, this.transform)
        {
            autoExpand = this.autoExpand
        };
    }
 

    public SlimeGameObject CreateSlime()
    {
        var sparksFx = this.pool.GetFreeElement();
        return sparksFx;
    }
}
