using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DynamicCanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textInteractableMessage;
    [SerializeField] private TextMeshProUGUI bulletsLoadedInPistol;
    [SerializeField] private TextMeshProUGUI bulletsInPlayerInventory;
    [SerializeField] private GameObject deathPanel;
    private Animation treatmentAnimation;
    private int lastBulletsInPistol;
    private int lastBulletsInInventory;

    private void Start()
    {
        treatmentAnimation = GetComponent<Animation>();
    }

    public void UpdateTextMessage(string message)
    {
        textInteractableMessage.text = message;
    }

    public void UpdateBullets(int bulletsInPistol, int bulletsInInventory)
    {
        if (lastBulletsInPistol != bulletsInPistol)
        {
            bulletsLoadedInPistol.rectTransform.DOShakeAnchorPos(0.25f, Vector2.one * 10, 5, 180f);
        }
        if (lastBulletsInInventory != bulletsInInventory)
        {
            bulletsInPlayerInventory.rectTransform.DOShakeAnchorPos(0.25f, Vector2.one * 10, 5, 180f);
        }
        
        bulletsLoadedInPistol.text = bulletsInPistol.ToString();
        bulletsInPlayerInventory.text = bulletsInInventory.ToString();
        lastBulletsInPistol = bulletsInPistol;
        lastBulletsInInventory = bulletsInInventory;
    }

    public void ActivateDeathPanel()
    {
        deathPanel.gameObject.SetActive(true);
        deathPanel.GetComponent<Image>().DOFade(1f, 1f).SetUpdate(true);
    }

    public void ReloadLevel() // OnClick event
    {
        SceneManager.LoadScene(0);
    }

    public void ActivateTreatmentEffect()
    {
        treatmentAnimation.Play();
    }
}
