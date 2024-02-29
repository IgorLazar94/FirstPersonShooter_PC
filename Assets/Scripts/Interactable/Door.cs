using System;
using DG.Tweening;
using Interactable;
using Interactable.Lightning;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactionMessage = "press E to activate the action";
    [SerializeField] private float targetAngle;
    [SerializeField] private bool isDoorLocked = false;
    [SerializeField] private bool isElectricDoor = false;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;
    [SerializeField] private AudioClip isClosedDoor;
    private AudioSource audioSource;
    private TextMeshPro electricDoorInfo;
    private bool isDoorOpen = false;
    private Vector3 isCloseRotation;
    private Vector3 isOpenRotation;

    private void OnEnable()
    {
        ElectricShield.SwitchElectricity += SwitchElectricDoorLocked;
    }

    private void OnDisable()
    {
        ElectricShield.SwitchElectricity -= SwitchElectricDoorLocked;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        electricDoorInfo = GetComponentInChildren<TextMeshPro>();
        var rotation = transform.rotation;
        isOpenRotation = new Vector3(rotation.x, targetAngle, rotation.z);
        isCloseRotation = new Vector3(rotation.x, rotation.y, rotation.z);
    }

    public string GetInteractionPlayerMessage()
    {
        return interactionMessage;
    }

    public void ActivateAction()
    {
        if (isDoorLocked)
        {
            ShakeDoor();
            return;
        }

        if (isDoorOpen)
        {
            audioSource.clip = doorOpen;
            audioSource.Play();
            transform.DOLocalRotate(isCloseRotation, 1f);
            isDoorOpen = false;
        }
        else
        {
            audioSource.clip = doorClose;
            audioSource.Play();
            transform.DOLocalRotate(isOpenRotation, 1f);
            isDoorOpen = true;
        }
    }

    private void ShakeDoor()
    {
        audioSource.clip = isClosedDoor;
        audioSource.Play();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, targetAngle, 0f), 0.2f));
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, -targetAngle, 0f), 0.2f));
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, targetAngle, 0f), 0.2f));
    }

    private void SwitchElectricDoorLocked(bool isHasElectricity)
    {
        if (isElectricDoor)
        {
            isDoorLocked = !isHasElectricity;
            if (isDoorLocked)
            {
                electricDoorInfo.text = "Locked";
                electricDoorInfo.color = Color.red;
                targetAngle = 0f;
            }
            else
            {
                electricDoorInfo.text = "Open";
                electricDoorInfo.color = Color.green;
                targetAngle = 90f;
            }
        }
    }
}