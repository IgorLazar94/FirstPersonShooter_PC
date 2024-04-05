using UnityEngine;
using Zenject;

public class SlimeObjectPool : MonoBehaviour
{
    private SlimeGameObject slimePrefab;
    private int poolCount = 7;
    private bool autoExpand = true;

    private ObjectPool<SlimeGameObject> pool;

    [Inject]
    private void Construct(SlimeGameObject slimePrefab)
    {
        this.slimePrefab = slimePrefab;
    }

    private void Start()
    {
        this.pool = new ObjectPool<SlimeGameObject>(slimePrefab, poolCount, this.transform)
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
