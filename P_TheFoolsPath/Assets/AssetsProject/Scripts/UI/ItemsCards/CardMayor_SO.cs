using NUnit.Framework;
using Unity.Burst.CompilerServices;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Mayor")]
public class CardMayor_SO : CardsData_SO
{
    [SerializeField]private CardMinor_SO[] _cardMinorList;


    public CardMinor_SO[] CardMinorList => _cardMinorList;
}
