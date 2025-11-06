using NUnit.Framework;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Mayor")]
public class CardMayor_SO : CardsData_SO
{
    [SerializeField] private CardsData_SO[] _relatedCards;

    [SerializeField] private Sprite _buttonGuessingSprite;

    public Sprite SpriteAdivination => _buttonGuessingSprite;
    public CardsData_SO[] RelatedMinorCards => _relatedCards;

}
