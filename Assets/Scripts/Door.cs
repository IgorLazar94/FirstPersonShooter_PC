using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactionMessage = "press E to activate the action";
    private bool isDoorOpen = false;
    [SerializeField] private float targetAngle;
    private Vector3 isCloseRotation;
    private Vector3 isOpenRotation;

    private void Start()
    {
        isOpenRotation = new Vector3(transform.rotation.x, targetAngle, transform.rotation.z);
        isCloseRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

    public string GetInteractionPlayerMessage()
    {
        return interactionMessage;
    }

    public void ActivateAction()
    {
        if (isDoorOpen)
        {
            transform.DOLocalRotate(isCloseRotation, 1f);
            isDoorOpen = false;
        }
        else
        {
            transform.DOLocalRotate(isOpenRotation, 1f);
            isDoorOpen = true;
        }
    }
}
