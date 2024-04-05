using Interactable;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private DynamicCanvasController dynamicCanvas;
        private float interactionDistance = 1.5f;
        private IInteractable currentInteractableObject;
        private Camera mainCamera;

        [Inject]
        private void Construct(DynamicCanvasController newDynamicCanvas)
        {
            dynamicCanvas = newDynamicCanvas;
        }
        
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
}
