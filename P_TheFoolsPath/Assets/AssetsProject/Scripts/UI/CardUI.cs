using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Image _cardImage;
    [SerializeField] private Button _cardButton;

    [ReadOnly, SerializeField] private CardRuntime _cardRuntime;

    public CardRuntime CardRuntime => _cardRuntime;

    private void Start()
    {
        if (_cardButton != null)
            _cardButton.onClick.AddListener(OnCardClicked);
    }


    public void SetupCardUI(CardRuntime cardRuntime)
    {
        _cardRuntime = cardRuntime;
        _cardImage.sprite = cardRuntime.CardData_SO.Sprite;
    }

    public void SetUnlocked(bool isUnlocked)
    {
        // Example logic to visually indicate locked/unlocked state
        _cardImage.color = isUnlocked ? Color.white : Color.gray;
    }
    private void OnCardClicked()
    {
        Debug.Log($"Card {_cardRuntime.CardName} clicked!");
        if (_cardRuntime == null)
        {
            Debug.LogWarning("CardRuntime is null. Cannot unlock card.");
            return;
        }
        CardsManager.Instance.SetCardAsUnlocked(_cardRuntime.Id);

    }
}
