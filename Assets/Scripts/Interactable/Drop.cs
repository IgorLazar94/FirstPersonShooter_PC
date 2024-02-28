using UnityEngine;

namespace Interactable
{
    public abstract class Drop : MonoBehaviour, IInteractable
    {
        protected PlayerState player;
        private readonly string message = "press E to pick up the drop";

        public void SetPlayerToDrop(PlayerState playerState)
        {
            this.player = playerState;
        }
        public virtual string GetInteractionPlayerMessage()
        {
            return message;
        }

        public virtual void ActivateAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
