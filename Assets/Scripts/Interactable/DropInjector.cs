using Player;
using UnityEngine;
using Zenject;

namespace Interactable
{
    public class DropInjector : MonoBehaviour
    {
        private PlayerState player;
        private Drop[] dropsArray;

        [Inject]
        private void Construct(PlayerState playerState)
        {
            player = playerState;
        }
        private void Start()
        {
            dropsArray = GetComponentsInChildren<Drop>();
            foreach (var drop in dropsArray)
            {
                drop.SetPlayerToDrop(player);
            }
        }
    }
}
