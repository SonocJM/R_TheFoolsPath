using System.Collections.Generic;
using Dino.UtilityTools.Singleton;
using NaughtyAttributes;
using UnityEngine;

public class CardsManager : Singleton<CardsManager>
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<CardMayor_SO> _cardMayorList;
    [SerializeField] private List<CardMinor_SO> _cardMinorList;
    [SerializeField]private List<CardRuntime> _cardInventory = new List<CardRuntime>();
    
    private CardMayor_SO _randomCardData;
    public UIManager UIManager => uiManager;

    public List<CardMayor_SO> CardMayorList => _cardMayorList;
    public List<CardMinor_SO> CardMinorList => _cardMinorList;
    public List<CardRuntime> CardInventory => _cardInventory;
    public CardMayor_SO SelectedMayorCard => _randomCardData;

    void Start()
    {
        List<CardRuntime> unLockedCard  = new List<CardRuntime>();
        
        string path = Application.persistentDataPath + "/cardsInventory.json";
        if (!System.IO.File.Exists(path))
        {
            Debug.LogWarning("No saved Cards invetory found" + path);
        }
        else 
        {
            //Read Cards on the json
        
            //unLockedCard =
        
        }


        
        
        
        int random = Random.Range(0, _cardMayorList.Count);
        _randomCardData = _cardMayorList[random];

    }
}
