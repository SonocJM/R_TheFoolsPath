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

    public override void Initialize()
    {
        Hide(true);
    }
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


    public void CreateCards(List<CardsData_SO> items)
    {
        foreach (var item in items)
        {
            SpawnCards(item);
        }

    }

    private void SpawnCards(CardsData_SO itemData)
    {
        GameObject go = Instantiate(cardPrefab, content.transform);
        InventoryCards item = go.GetComponent<InventoryCards>();
        item.SetInfo(itemData.Sprite);
    }
}
