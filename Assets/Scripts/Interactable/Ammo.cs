using MenuScene;

namespace Interactable
{
    public class Ammo : Drop
    {
        private const int BulletsInAmmoBox = 14;
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
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                interactionMessage = interactionMessageEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                interactionMessage = interactionMessageUa;
            }
        }
    }
}
