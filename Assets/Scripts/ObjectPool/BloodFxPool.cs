using UnityEngine;

namespace ObjectPool
{
    public class BloodFxPool : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] bloodPoolArray;
        private int currentPoolElementID;
        private readonly int poolCount = 4;

        private void Start()
        {
            foreach (var bloodFx in bloodPoolArray)
            {
                bloodFx.gameObject.SetActive(false);
            }
        }

        public void InitNewBlood(Vector3 particleTransPos)
        {
            bloodPoolArray[currentPoolElementID].gameObject.SetActive(true);
            bloodPoolArray[currentPoolElementID].transform.position = particleTransPos;
            bloodPoolArray[currentPoolElementID].Play();
            currentPoolElementID++;
            if (currentPoolElementID > poolCount - 1) currentPoolElementID = 0;
        }
    }
}
