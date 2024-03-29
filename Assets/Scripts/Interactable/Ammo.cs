using MenuScene;
using Zenject;

namespace Interactable
{
    public class Ammo : Drop
    {
        private const int BulletsInAmmoBox = 7;
        private readonly string interactionMessageEn = "Press E to pickup ammo box";
        private readonly string interactionMessageUa = "Press E to pickup ammo box";
        private string interactionMessage;

        private void Start()
        {
            CheckLocalization();
        }

        public override string GetInteractionPlayerMessage()
        {
            return interactionMessage;
        }

        public override void ActivateAction()
        {
            player.AddPlayerAmmo(BulletsInAmmoBox);
            Destroy(this.gameObject);
        }
        
        private new void CheckLocalization()
        {
            if (localizationController.GetCurrentLocalization() == TypeOfLocalization.English)
            {
                interactionMessage = interactionMessageEn;
            }
            else if (localizationController.GetCurrentLocalization() == TypeOfLocalization.Ukrainian)
            {
                interactionMessage = interactionMessageUa;
            }
        }
    }
}
