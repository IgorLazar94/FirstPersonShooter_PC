using Player;
using UnityEngine;
using MenuScene;

namespace Interactable
{
    public abstract class Drop : MonoBehaviour, IInteractable
    {
        protected PlayerState player;
        private readonly string interactionMessageEn = "press E to pick up the drop";
        private readonly string interactionMessageUa = "натисніть Е, щоб взяти дроп";
        private string actualMessage;
        
        public void SetPlayerToDrop(PlayerState playerState)
        {
            this.player = playerState;
        }
        public virtual string GetInteractionPlayerMessage()
        {
            return actualMessage;
        }

        public virtual void ActivateAction()
        {
            throw new System.NotImplementedException();
        }

        public void CheckLocalization()
        {
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                actualMessage = interactionMessageEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                actualMessage = interactionMessageUa;
            }
        }
    }
}
