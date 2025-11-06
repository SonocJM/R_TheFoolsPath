using DG.Tweening;
using NaughtyAttributes;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIWindow
{
    [Header("Inventory UI")]
    [BoxGroup("Inventory")][SerializeField] private GameObject cardPrefab;
    [BoxGroup("Inventory")][SerializeField] private GameObject content;

    public RectTransform inventoryContainer;
    public CanvasGroup canvasGroup;

    public int yDistanceShow;
    public int yDistanceHide;
    public float fadeDuration;

    private List<CardUI> cardsInInventoryUI = new List<CardUI>();

    public override void Initialize()
    {
        Hide(true);
    }
    [Button]
    public override void Show(bool instant = false)
    {
        gameObject.SetActive(true);

        if (instant)
        {
            inventoryContainer.anchoredPosition = new Vector2(inventoryContainer.anchoredPosition.x,yDistanceShow);
            canvasGroup.alpha = 1f;
        }
        else
        {
            inventoryContainer.DOAnchorPosY(yDistanceShow, AnimationTime);
            canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.Linear);
        }
    }
    [Button]
    public override void Hide(bool instant = false)
    {


        if (instant)
        {
            canvasGroup.alpha = 0f;
            inventoryContainer.anchoredPosition = new Vector2(inventoryContainer.anchoredPosition.x,yDistanceHide);
            gameObject.SetActive(false);
        }
        else
        {
            inventoryContainer.DOAnchorPosY(yDistanceHide, AnimationTime);
            canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
        }
    }


    public void SetUpCardsInInventory(List<CardRuntime> cardsInInventory)
    {
        // Clear existing UI elements
        foreach (var cardUI in cardsInInventoryUI)
        {
            Destroy(cardUI.gameObject);
        }
        cardsInInventoryUI.Clear();

        // Create new UI elements for each card in the inventory
        foreach (var cardRuntime in cardsInInventory)
        {
            GameObject go = Instantiate(cardPrefab, content.transform);
            CardUI cardUI = go.GetComponent<CardUI>();
            cardUI.SetupCardUI(cardRuntime);
            cardUI.SetUnlocked(cardRuntime.IsUnlocked);
            cardsInInventoryUI.Add(cardUI);
        }

    }
}
