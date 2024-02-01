using UnityEngine;

public enum TypeOfDoor
{
    Left,
    Right
}
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private TypeOfDoor typeOfDoor;
    public string interactionMessage = "press E to activate the action";
    private bool isDoorOpen = false;

    public string GetInteractionPlayerMessage()
    {
        return interactionMessage;
    }

    public void ActivateAction()
    {
        OpenDoor();
    }

    private void OpenDoor()
    {
        switch (typeOfDoor)
        {
            case TypeOfDoor.Left:
                break;
            case TypeOfDoor.Right:
                break;
            default:
                break;
        }
    }
}
