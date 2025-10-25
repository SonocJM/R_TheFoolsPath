using UnityEngine;
using System;

[Serializable]
public class CardRuntime
{
    public CardsData_SO CardData;
    public string ID;
    public bool IsUnlocked;

    public CardRuntime(CardsData_SO cardData, string iD, bool isUnlocked)
    {
        CardData = cardData;
        ID = iD;
        IsUnlocked = isUnlocked;
    }
}
