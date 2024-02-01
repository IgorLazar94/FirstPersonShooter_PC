using TMPro;
using UnityEngine;

public class DynamicCanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMessage;

    public void UpdateTextMessage(string message)
    {
        textMessage.text = message;
    }
}
