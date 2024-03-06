using Player;
using UnityEngine;

namespace TriggerScenarioZones
{
    public class PipeExplosion : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PipeSystem.PipeSystem pipeSystem;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerState player))
            {
                gameManager.SetNewScenarioStage(GameScenarioLevel.BurstPipe);
                pipeSystem.StartFire();
            }
        }
    }
}
