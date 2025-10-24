using System;
using System.Collections.Generic;
using Dino.UtilityTools.Singleton;
using NaughtyAttributes;
using UnityEngine;

public class CardsManager : Singleton<CardsManager>
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<CardsData_SO> cards;
    public UIManager UIManager => uiManager;

    public List<CardsData_SO> Cards => cards;

    [Button]
    private void CreateAllItems()
    {
        InventoryUI inventoryUI = UIManager.Instance.GetUIWindow(WindowsIDs.Inventory) as InventoryUI;
        if (inventoryUI == null) return;
        inventoryUI.CreateCards(cards);
    }
}
