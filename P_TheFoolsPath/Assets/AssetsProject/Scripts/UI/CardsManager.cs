using System;
using System.Collections.Generic;
using System.Linq;
using Dino.UtilityTools.Singleton;
using UnityEngine;
using NaughtyAttributes;
public class CardsManager : Singleton<CardsManager>
{
    [Header("Cards Manager")]
    [SerializeField]
    private List<CardsData_SO> cardDatas;

    [Header("Inventory")][ReadOnly] public List<CardRuntime> CardsInventory = new List<CardRuntime>();

    [Header("Unlocked and Locked Cards")]
    [ReadOnly]
    public List<CardRuntime> UnlockedCards = new List<CardRuntime>();

    [ReadOnly] public List<CardRuntime> LockedCards = new List<CardRuntime>();

    private CardRuntime _selectedMayorCard;

    void Start()
    {
        //First Create All Cards to the Inventory
        //Cards are LOCKED by DEFAULT
        CreateAllCards();

        //Then Find Unlocked Cards from Save and set them to UNLOCKED true
        //Here we update the CardsInventory list and also fill the UnlockedCards list
        FindUnlockedCardsFromSave();

        //Update the LockedCards list
        //From this list we are going to select random locked cards to unlock
        UpdateLockedCardsList();

        // Select a random Mayor locked card
        _selectedMayorCard = SelectRandomMayorLockedCard();

        //Update Cards in the Table UI based on the selected Mayor card
        UpdateCardsInTable();
    }

    private void CreateAllCards()
    {
        foreach (var cardData in cardDatas)
        {
            CardRuntime newCard = new CardRuntime(
                cardData,
                cardData.ID,
                cardData.CardName,
                // All cards are locked by default
                false
            );
            CardsInventory.Add(newCard);
        }
    }

    private void FindUnlockedCardsFromSave()
    {
        string path = Application.persistentDataPath + "/cardsInventory.json";
        Debug.Log(path);
        if (!System.IO.File.Exists(path))
        {
            Debug.LogWarning("No saved Cards Inventory found at: " + path);
            return;
        }

        string json = System.IO.File.ReadAllText(path);
        CardRuntime[] cards = JsonHelper.FromJson<CardRuntime>(json);

        foreach (var cardsFromSave in cards)
        {
            if (cardsFromSave.IsUnlocked)
            {
                //Find the card in the CardsInventory and set it to Unlocked
                CardRuntime cardInInventory = CardsInventory.Find(card => card.Id == cardsFromSave.Id);
                if (cardInInventory != null)
                {
                    //Set card from inventory to Unlocked
                    cardInInventory.IsUnlocked = true;
                    //Add to UnlockedCards list
                    UnlockedCards.Add(cardInInventory);
                }
            }
        }
    }

    private void UpdateLockedCardsList()
    {
        LockedCards.Clear();
        foreach (var card in CardsInventory)
        {
            if (!card.IsUnlocked)
            {
                LockedCards.Add(card);
            }
        }
    }

    private CardRuntime SelectRandomMayorLockedCard()
    {
        //Filter LockedCards to get only Mayor cards
        List<CardRuntime> mayorLockedCards = LockedCards.FindAll(card => card.CardData_SO as CardMayor_SO);

        if (mayorLockedCards.Count == 0)
        {
            Debug.Log("No locked Mayor cards available.");
            return null;
        }

        //Select a random Mayor locked card
        CardRuntime selectedCard = mayorLockedCards[UnityEngine.Random.Range(0, mayorLockedCards.Count)];

        Debug.Log("Selected Mayor Card: " + selectedCard.CardName);
        return selectedCard;
    }

    private void UpdateCardsInTable()
    {
        //Get CardsTable UI
        GameplayUI cardsTableUI = UIManager.Instance.GetUIWindow(WindowsIDs.Gameplay) as GameplayUI;
        if (cardsTableUI == null) return;

        //Get related cards from the selected Mayor card
        CardMayor_SO mayorSo = _selectedMayorCard.CardData_SO as CardMayor_SO;
        if (mayorSo == null) return;

        //Find related CardRuntime from CardsInventory
        List<CardRuntime> relatedCards = new List<CardRuntime>();
        foreach (var relatedCardData in mayorSo.RelatedMinorCards)
        {
            CardRuntime relatedCardRuntime = CardsInventory.Find(card => card.CardData_SO == relatedCardData);
            if (relatedCardRuntime != null)
            {
                relatedCards.Add(relatedCardRuntime);
                Debug.Log("si hay cartas");
            }
        }

        //Set up cards on the table
        cardsTableUI.SetUpCardsOnTable(relatedCards);
    }

    [Button]
    public void SaveCardsInventory()
    {
        List<CardRuntime> cards = CardsInventory;

        string json = JsonHelper.ToJson(cards.ToArray(), true);
        string path = Application.persistentDataPath + "/cardsInventory.json";
        System.IO.File.WriteAllText(path, json);
        Debug.Log("Cards Inventory saved to: " + path);
    }

    public void SetCardAsUnlocked(string cardId)
    {
        //Find Card in CardsInventory
        CardRuntime cardInInventory = CardsInventory.Find(c => c.Id == cardId);

        //Set card as unlocked
        cardInInventory.IsUnlocked = true;

        //Add to UnlockedCards list
        if (!UnlockedCards.Contains(cardInInventory))
        {
            UnlockedCards.Add(cardInInventory);
        }

        //Remove from LockedCards list
        if (LockedCards.Contains(cardInInventory))
        {
            LockedCards.Remove(cardInInventory);
        }

        Debug.Log("Card Unlocked: " + cardInInventory.Id);

        //Save Inventory after unlocking a card
        SaveCardsInventory();
    }


    [Button]
    private void ShowCardsInventory()
    {
        InventoryUI cardsInventoryUI = UIManager.Instance.GetUIWindow(WindowsIDs.Inventory) as InventoryUI;
        if (cardsInventoryUI == null) return;
        //Filter mayor cards
        cardsInventoryUI.SetUpCardsInInventory(CardsInventory);
        UIManager.Instance.ShowUI(WindowsIDs.Inventory);
    }

    public List<CardRuntime> GetMayorCardsLocked()
    {
        List<CardRuntime> mayorLockedCards = LockedCards.FindAll(card => card.CardData_SO as CardMayor_SO);
        return mayorLockedCards;
    }

}
