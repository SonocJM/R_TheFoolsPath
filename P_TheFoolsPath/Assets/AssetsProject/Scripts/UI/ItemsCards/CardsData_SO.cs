using UnityEngine;
public class CardsData_SO : ScriptableObject
{
    [SerializeField]private string _id;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private string _description;


    public string ID => _id;
    public Sprite Sprite => _sprite;

    public string Name => _name;

    public string Description => _description;
}
