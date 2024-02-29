using Player;
using UnityEngine;

namespace Interactable
{
    public class DropInjector : MonoBehaviour
    {
        [SerializeField] private PlayerState player;
        private Drop[] dropsArray;

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
