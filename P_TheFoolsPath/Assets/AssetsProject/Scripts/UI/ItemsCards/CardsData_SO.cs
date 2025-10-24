using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class CardsData_SO : ScriptableObject
{
    private string _id;
    private Sprite _sprite;


    public string ID => _id;
    public Sprite sprite => _sprite;
}
