using MenuScene;

namespace Interactable
{
    public class FirstAidKid : Drop
    {
        private const int AddHealthPoints = 20;
        private const string InteractionMessageEn = "Press E to pickup first aid kit";
        private const string InteractionMessageUa = "Наптисніть Е, щоб підібрати аптечку";
        private string actualFirstAidMessage;


        private void Start()
        {
            LocalCheckLocalization();
        }

        public override string GetInteractionPlayerMessage()
        {
            return actualFirstAidMessage;
        }

        public override void ActivateAction()
        {
            player.AddPlayerHealth(AddHealthPoints);
            Destroy(this.gameObject);
        }
        
        private void LocalCheckLocalization()
        {
            if (LocalizationController.currentLocalization == TypeOfLocalization.English)
            {
                actualFirstAidMessage = InteractionMessageEn;
            }
            else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
            {
                actualFirstAidMessage = InteractionMessageUa;
            }
        }
    }
}
