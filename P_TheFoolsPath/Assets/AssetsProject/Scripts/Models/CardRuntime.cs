using UnityEngine;
using System;

[Serializable]
public class CardRuntime
{
    public string ID;
    public bool IsUnlocked;

    public CardRuntime( string iD, bool isUnlocked)
    {
        ID = iD;
        IsUnlocked = isUnlocked;
    }
}
