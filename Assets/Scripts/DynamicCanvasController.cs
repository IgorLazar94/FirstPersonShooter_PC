using TMPro;
using UnityEngine;
using DG.Tweening;

public class DynamicCanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textInteractableMessage;
    [SerializeField] private TextMeshProUGUI bulletsLoadedInPistol;
    [SerializeField] private TextMeshProUGUI bulletsInPlayerInventory;
    private int lastBulletsInPistol;
    private int lastBulletsInInventory;
    public void UpdateTextMessage(string message)
    {
        textInteractableMessage.text = message;
    }

    public void UpdateBullets(int bulletsInPistol, int bulletsInInventory)
    {
        if (lastBulletsInPistol != bulletsInPistol)
        {
            bulletsLoadedInPistol.rectTransform.DOShakeAnchorPos(0.25f, Vector3.one * 10, 5, 180f);
        }
        if (lastBulletsInInventory != bulletsInInventory)
        {
            bulletsInPlayerInventory.rectTransform.DOShakeAnchorPos(0.25f, Vector3.one, 5, 180f);
        }
        
        bulletsLoadedInPistol.text = bulletsInPistol.ToString();
        bulletsInPlayerInventory.text = bulletsInInventory.ToString();
        lastBulletsInPistol = bulletsInPistol;
        lastBulletsInInventory = bulletsInInventory;
    }
}
