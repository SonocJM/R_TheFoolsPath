using System;
using System.Collections.Generic;
using Dino.UtilityTools.Singleton;
using NaughtyAttributes;
using UnityEngine;

public class CardsManager : Singleton<CardsManager>
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<CardMayor_SO> _cardMayorList;
    [SerializeField] private List<CardMinor_SO> _cardMinorList;
    public UIManager UIManager => uiManager;

    public List<CardMayor_SO> CardMayorList => _cardMayorList;
    public List<CardMinor_SO> CardMinorList => _cardMinorList;

    [Button]
    private void CreateAllItems()
    {

        InventoryUI inventoryUI = UIManager.Instance.GetUIWindow(WindowsIDs.Inventory) as InventoryUI;
        if (inventoryUI == null) return;
        //inventoryUI.CreateCards(CardMayorList);
    }
}
