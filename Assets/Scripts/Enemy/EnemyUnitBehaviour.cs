using ModularFirstPersonController.FirstPersonController;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyUnitBehaviour : MonoBehaviour
    {
        protected FirstPersonController player;

        [Inject]
        private void Construct(FirstPersonController playerFPS)
        {
            player = playerFPS;
        }
    }
}
