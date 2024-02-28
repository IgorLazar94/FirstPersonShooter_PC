using UnityEngine;

namespace Interactable
{
    public class Ammo : Drop
    {
        private const int BulletsInAmmoBox = 14;
        private const string InteractionMessage = "Press E to pickup ammo box";

        public override string GetInteractionPlayerMessage()
        {
            return InteractionMessage;
        }

        public override void ActivateAction()
        {
            player.AddPlayerAmmo(BulletsInAmmoBox);
            Destroy(this.gameObject);
        }
    }
}
