using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SparksPool : MonoBehaviour
{
    private SparksGameObject sparksFxPrefab;
    private int poolCount = 7;
    private bool autoExpand = true;

    private ObjectPool<SparksGameObject> pool;

    [Inject]
    private void Construct(SparksGameObject sparksFxPrefab)
    {
        this.sparksFxPrefab = sparksFxPrefab;
    }
    private void Start()
    {
        this.pool = new ObjectPool<SparksGameObject>(sparksFxPrefab, poolCount, this.transform)
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
