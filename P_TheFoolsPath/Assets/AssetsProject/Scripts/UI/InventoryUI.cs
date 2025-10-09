using NaughtyAttributes;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIWindow
{
    [Header("Inventory UI")]
    [BoxGroup("Inventory")][SerializeField] private GameObject cardPrefab;
    [BoxGroup("Inventory")][SerializeField] private GameObject content;

    public void CreateCards(List<CardsData> items)
    {
        foreach (var item in items)
        {
            SpawnCards(item);
        }

    }

    private void SpawnCards(CardsData itemData)
    {
        GameObject go = Instantiate(cardPrefab, content.transform);
        InventoryCards item = go.GetComponent<InventoryCards>();
        item.SetInfo(itemData.sprite, itemData.text);
    }
}
