using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private DynamicCanvasController dynamicCanvas;
    private float interactionDistance = 1.5f;
    private IInteractable currentInteractableObject;

    void Update()
    {
        CheckInteractionObject();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractableObject != null)
        {
            currentInteractableObject.ActivateAction();
        }
    }

    private void CheckInteractionObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                string message = interactable.GetInteractionPlayerMessage();
                UpdateInteractionText(message);
                currentInteractableObject = interactable;
            }
            else
            {
                ClearInteractionText();
            }
        }
        else
        {
            ClearInteractionText();
        }
    }

    private void UpdateInteractionText(string message)
    {
        dynamicCanvas.UpdateTextMessage(message);
    }

    private void ClearInteractionText()
    {
        dynamicCanvas.UpdateTextMessage("");
        currentInteractableObject = null;
    }
}
