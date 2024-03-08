using System.Collections;
using DG.Tweening;
using Interactable;
using Interactable.Lightning;
using TMPro;
using UnityEngine;
using MenuScene;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float targetAngle;
    [SerializeField] private bool isDoorLocked = false;
    [SerializeField] private bool isElectricDoor = false;
    private readonly string interactionMessageEn = "press E to open the door";
    private readonly string interactionMessageUa = "Натисніть Е, щоб відчинити двері";
    private string actualMessage;
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
        CheckLocalization();
        audioSource = GetComponent<AudioSource>();
        electricDoorInfo = GetComponentInChildren<TextMeshPro>();
        var rotation = transform.rotation;
        isOpenRotation = new Vector3(rotation.x, targetAngle, rotation.z);
        isCloseRotation = new Vector3(rotation.x, rotation.y, rotation.z);
    }

    public string GetInteractionPlayerMessage()
    {
        return actualMessage;
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
            StartCoroutine(PlayDoorSoundWithDelay(DoorSoundCollection.instance.DoorOpen, 0.4f));
            transform.DOLocalRotate(isCloseRotation, 1f);
            isDoorOpen = false;
        }
        else
        {
            StartCoroutine(PlayDoorSoundWithDelay(DoorSoundCollection.instance.DoorClose, 0.7f));
            transform.DOLocalRotate(isOpenRotation, 1f);
            isDoorOpen = true;
        }
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

    private IEnumerator PlayDoorSoundWithDelay(AudioClip sound, float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.clip = sound;
        audioSource.Play();
    }

    private void ShakeDoor()
    {
        StartCoroutine(PlayDoorSoundWithDelay(DoorSoundCollection.instance.DoorLocked, 0f));
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, targetAngle, 0f), 0.2f));
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, -targetAngle, 0f), 0.2f));
        sequence.Append(transform.DOLocalRotate(new Vector3(0f, targetAngle, 0f), 0.2f));
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