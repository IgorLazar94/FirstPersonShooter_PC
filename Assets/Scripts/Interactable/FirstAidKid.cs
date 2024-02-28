using UnityEngine;

namespace Interactable
{
    public class FirstAidKid : Drop
    {
        private const int AddHealthPoints = 20;
        private const string InteractionMessage = "Press E to pickup first aid kit";

        public override string GetInteractionPlayerMessage()
        {
            return InteractionMessage;
        }

        public override void ActivateAction()
        {
            player.AddPlayerHealth(AddHealthPoints);
            Destroy(this.gameObject);
        }
    }
}
