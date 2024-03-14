using System;
using DG.Tweening;
using MenuScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadControl : MonoBehaviour
{
    [SerializeField] private Image loadSprite;
    [SerializeField] private TextMeshProUGUI loadText;
    private string loadTextEn = "Loading";
    private string loadTextUa = "Завантаження";
    private string actualText;
    private float rotationAngle = 45f;
    private float interval = 0.33f;
    private int maxDots = 3;
    private int currentDots = 0;

    public void InitLoadPanel()
    {
        CheckLocalization();
        RotateLoadSprite();
        AnimateLoadText();
    }

    private void RotateLoadSprite()
    {
        Sequence rotationSequence = DOTween.Sequence();

        rotationSequence.Append(loadSprite.rectTransform.DORotate(new Vector3(0, 0, -rotationAngle), interval / 2f))
            .SetEase(Ease.InOutQuad);
        rotationSequence.AppendInterval(0.5f);
        rotationSequence.SetLoops(-1);
    }

    private void AnimateLoadText()
    {
        Sequence textSequence = DOTween.Sequence();
        textSequence.AppendInterval(interval * 2f)
            .AppendCallback(() =>
            {
                currentDots = (currentDots + 1) % (maxDots + 1);
                string dots = new string('.', currentDots);
                loadText.text = actualText + dots;
            })
            .SetLoops(-1);
    }

    private void CheckLocalization()
    {
        if (LocalizationController.currentLocalization == TypeOfLocalization.English)
        {
            actualText = loadTextEn;
            SetNewLoadTextPos(-80f);
        }
        else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
        {
            actualText = loadTextUa;
            SetNewLoadTextPos(-195f);
        }
        else
        {
            actualText = loadTextEn;
            SetNewLoadTextPos(-80f);
        }

        loadText.text = actualText;
    }

    private void SetNewLoadTextPos(float newPosX)
    {
        Vector2 newPosition = loadText.rectTransform.anchoredPosition;
        newPosition.x = newPosX;
        loadText.rectTransform.anchoredPosition = newPosition;
    }
}