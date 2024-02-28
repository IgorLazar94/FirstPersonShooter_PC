using System;
using Interactable;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private DynamicCanvasController dynamicCanvas;
    private float interactionDistance = 1.5f;
    private IInteractable currentInteractableObject;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                Debug.Log("see interectable");
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
