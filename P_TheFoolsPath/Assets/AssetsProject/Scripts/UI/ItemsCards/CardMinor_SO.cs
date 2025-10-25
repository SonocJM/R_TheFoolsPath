using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Minor")]
public class CardMinor_SO : CardsData_SO
{
    [SerializeField] private string _hint;
    [SerializeField] private CardMayor_SO _cardMayor;

    public string Hint => _hint;

    public CardMayor_SO CardMayor => _cardMayor;
}
