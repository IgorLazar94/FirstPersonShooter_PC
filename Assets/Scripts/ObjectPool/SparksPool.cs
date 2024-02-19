using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksPool : MonoBehaviour
{
    [SerializeField] private SparksGameObject sparksFxPrefab;
    private int poolCount = 7;
    private bool autoExpand = true;

    private BulletPool<SparksGameObject> pool;

    private void Start()
    {
        this.pool = new BulletPool<SparksGameObject>(sparksFxPrefab, poolCount, this.transform)
        {
            autoExpand = this.autoExpand
        };
    }
 

    public SparksGameObject CreateSparks()
    {
        var sparksFx = this.pool.GetFreeElement();
        return sparksFx;
    }
}
