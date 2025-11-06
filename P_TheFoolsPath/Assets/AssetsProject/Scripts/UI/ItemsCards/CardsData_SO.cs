using UnityEngine;
public class CardsData_SO : ScriptableObject
{
    //Scriptable objects only for data storage
    [SerializeField] private string _id;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string cardName;
    [SerializeField] private string description;

    public Sprite Sprite => sprite;
    public string CardName => cardName;
    public string Description => description;
    public string ID => _id;
}