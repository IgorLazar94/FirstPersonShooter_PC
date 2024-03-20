using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;
using MenuScene;

public class QuestSystem : MonoBehaviour
{
    public static Action OnUpdateQuest;
    public TextMeshProUGUI questTextPrefab;
    public Transform questListParent;
    private List<TextMeshProUGUI> oldQuestsList = new List<TextMeshProUGUI>();
    private float oldQuestSize = 40f;
    private float newQuestSize = 80f;
    private float topMargin = 20f;
    private float leftMargin = 500f;

    private int questCounter;

    // English content
    private string escapeTextEn = "Escape from the hospital";
    private string powerupElectricityEn = "Apply voltage to the output";
    private string turnOffGasEn = "Close the gas valves";

    // Ukrainian content
    private string escapeTextUa = "Тікай з лікарні";
    private string powerupElectricityUa = "Подай напругу на вихід";
    private string turnOffGasUa = "Перекрийте вентилі газу";

    // Actual content
    private string escapeTextActual;
    private string powerupElectricityActual;
    private string turnOffGasActual;

    private void OnEnable()
    {
        OnUpdateQuest += UpdateQuest;
    }

    private void OnDisable()
    {
        OnUpdateQuest -= UpdateQuest;
    }

    private void Awake()
    {
        CheckLocalization();
    }

    private void Start()
    {
        OnUpdateQuest?.Invoke();
    }

    private void AddQuest(string questName)
    {
        RepaintOldQuests();
        TextMeshProUGUI newQuestText = Instantiate(questTextPrefab, questListParent);
        newQuestText.text = questName;
        questCounter++;
        UpdateQuestTextStyle(newQuestText);
        oldQuestsList.Add(newQuestText);
        UpdateQuestTextPositions();
        AnimateNewQuest(newQuestText);
    }

    private void AnimateNewQuest([NotNull] TextMeshProUGUI newQuestText)
    {
        if (newQuestText == null) throw new ArgumentNullException(nameof(newQuestText));
        Vector2 newTextPos = new Vector2(leftMargin, -oldQuestsList.Count * oldQuestSize);
        Vector2 initialPosition = new Vector2(Screen.width / 2f, -Screen.height / 4f);
        newQuestText.rectTransform.anchoredPosition = initialPosition;

        newQuestText.rectTransform.DOScale(Vector3.one * 1.4f, 0.25f)
            .OnComplete(() => newQuestText.rectTransform.DOScale(Vector3.one, 0.25f));


        newQuestText.rectTransform.DOAnchorPos(newTextPos, 1f)
            .SetDelay(1f)
            .SetEase(Ease.OutQuad);
    }

    private void UpdateQuestTextStyle(TextMeshProUGUI questText)
    {
        questText.color = Color.white;
        questText.fontSize = newQuestSize;
    }

    private void UpdateQuestTextPositions()
    {
        for (int i = 0; i < oldQuestsList.Count; i++)
        {
            oldQuestsList[i].rectTransform.anchoredPosition = new Vector2(leftMargin, (-i * oldQuestSize) - topMargin);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnUpdateQuest?.Invoke();
        }
    }

    private void RepaintOldQuests()
    {
        foreach (var oldQuest in oldQuestsList)
        {
            oldQuest.color = Color.gray;
            oldQuest.fontSize = oldQuestSize;
            oldQuest.fontStyle = FontStyles.Strikethrough;
        }
    }

    private void CheckLocalization()
    {
        if (LocalizationController.currentLocalization == TypeOfLocalization.English)
        {
            escapeTextActual = escapeTextEn;
            powerupElectricityActual = powerupElectricityEn;
            turnOffGasActual = turnOffGasEn;
        }
        else if (LocalizationController.currentLocalization == TypeOfLocalization.Ukrainian)
        {
            escapeTextActual = escapeTextUa;
            powerupElectricityActual = powerupElectricityUa;
            turnOffGasActual = turnOffGasUa;
        }
    }

    public void UpdateQuest()
    {
        switch (questCounter)
        {
            case 0:
                AddQuest(escapeTextActual);
                break;
            case 1:
                AddQuest(powerupElectricityActual);
                break;
            case 2:
                AddQuest(escapeTextActual);
                break;
            case 3:
                AddQuest(turnOffGasActual);
                break;
            case 4:
                AddQuest(escapeTextActual);
                break;
            default:
                AddQuest(escapeTextActual);
                break;
        }
    }
}