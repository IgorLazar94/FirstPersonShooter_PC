using Player;
using UnityEngine;

namespace PipeSystem
{
    public class FireZone : MonoBehaviour
    {
        private readonly int damageFromFire = 15;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerState player))
            {
                player.PlayerTakeDamage(damageFromFire);
            }
        }
    }
}
