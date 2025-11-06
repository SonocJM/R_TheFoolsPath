using System;

[Serializable]
public class CardRuntime
{
    //Runtime representation of a card
    public CardsData_SO CardData_SO;
    public string Id;
    public string CardName;
    public bool IsUnlocked = false;


    public CardRuntime(CardsData_SO cardDataSo, string id, string cardName, bool isUnlocked)
    {
        CardData_SO = cardDataSo;
        CardName = cardName;
        IsUnlocked = isUnlocked;
        Id = id;
    }
}
