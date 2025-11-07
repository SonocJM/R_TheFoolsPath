using DG.Tweening;
using JetBrains.Annotations;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : UIWindow
{
    [Header("Cards:")]
    [SerializeField] private Image _card1;
    [SerializeField] private Image _card2;
    [SerializeField] private Image _card3;
    [SerializeField] private Image _card4;

    [SerializeField] private CardMayor_SO cardMayorData;

    [Header("Other:")]
    [SerializeField] private Image _slider;
    public CanvasGroup canvasGroup;

    [Header("Config Animation Show/Hide")]
    public int yDistanceCards;
    public float fadeDuration = 0.5f;

    [Header("Config Animation Shuffle Cards")]
    public float upDistance = 50f;
    public float rightDistance = 30f;
    public float moveDuration = 0.3f;
    public float delayBetweenCards = 0.1f;
    private bool isAnimating = false;
    private bool card1Turn = true;

    [Header("Config Panel Guessing")]
    [SerializeField] private GameObject guessingpanel;
    [SerializeField] private GameObject ButtonMayorContainer;
    [SerializeField] private GameObject PrefabButtonMayor;
    [SerializeField] private Button[] ButtonChoice;
    [SerializeField] private float buttonDelay = 0.05f;
    private bool panelVisible = false;
    private CardMayorButton cardMayorButton;

    private Image[] _cards;

    [Header("Cards Table UI")]
    [SerializeField] private List<CardUI> cardsOnTableUI = new List<CardUI>();
    public override void Initialize()
    {
        Hide(true);
        guessingpanel.SetActive(false);
    }

    [Button]
    public override void Show(bool instant = false)
    {
        WindowCanvas.gameObject.SetActive(true);

        if (instant)
        {
            _card1.rectTransform.DOAnchorPosY(-598, 0);
            _card2.rectTransform.DOAnchorPosY(-579, 0);
            _card3.rectTransform.DOAnchorPosY(-552, 0);
            _card4.rectTransform.DOAnchorPosY(-532, 0);

            canvasGroup.alpha = 1f;
        }
        else
        {
            _card1.rectTransform.DOAnchorPosY(-598, AnimationTime).SetEase(Ease.OutCubic);
            _card2.rectTransform.DOAnchorPosY(-579, AnimationTime).SetEase(Ease.OutCubic);
            _card3.rectTransform.DOAnchorPosY(-552, AnimationTime).SetEase(Ease.OutCubic);
            _card4.rectTransform.DOAnchorPosY(-532, AnimationTime).SetEase(Ease.OutCubic);

            canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.Linear);
        }
    }
    [Button]
    public override void Hide(bool instant = false)
    {

        if (instant)
        {
            _card1.rectTransform.DOAnchorPosY(yDistanceCards, 0);
            _card2.rectTransform.DOAnchorPosY(yDistanceCards, 0);
            _card3.rectTransform.DOAnchorPosY(yDistanceCards, 0);
            _card4.rectTransform.DOAnchorPosY(yDistanceCards, 0);

            canvasGroup.alpha = 0f;
            WindowCanvas.gameObject.SetActive(false);
        }
        else
        {
            _card1.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => WindowCanvas.gameObject.SetActive(false));
            _card2.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => WindowCanvas.gameObject.SetActive(false));
            _card3.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => WindowCanvas.gameObject.SetActive(false));
            _card4.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => WindowCanvas.gameObject.SetActive(false));

            canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        }
    }

    [Button("Shuffle Cards")]
    public void ShuffleCards()
    {
        if (isAnimating)
        {
            Debug.Log("Espera a que termine la animación.");
            return;
        }

        if (_cards == null || _cards.Length == 0)
            _cards = new Image[] { _card1, _card2, _card3, _card4 };

        isAnimating = true;

        Image activeCard = card1Turn ? _card1 : _card2;

        RectTransform rect = activeCard.rectTransform;
        Vector2 startPos = rect.anchoredPosition;

        float upTargetY = startPos.y + upDistance;
        float rightTargetX = startPos.x + rightDistance;

        DG.Tweening.Sequence seq = DOTween.Sequence();

        seq.Append(rect.DOAnchorPosY(upTargetY, moveDuration).SetEase(Ease.OutQuad))
           .Append(rect.DOAnchorPosX(rightTargetX, moveDuration).SetEase(Ease.OutQuad))
           .AppendCallback(() =>
           {
               activeCard.transform.SetAsFirstSibling();
           })
           .Append(rect.DOAnchorPosY(startPos.y, moveDuration).SetEase(Ease.InQuad))
           .Append(rect.DOAnchorPosX(startPos.x, moveDuration).SetEase(Ease.InQuad))
           .OnComplete(() =>
           {
               _card4.transform.SetAsFirstSibling();
               _card3.transform.SetAsFirstSibling();

               card1Turn = !card1Turn;
               isAnimating = false;
               Debug.Log("Barajado completado.");
           });

    }


    public void SetUpCardsOnTable(List<CardRuntime> cardsOnTable)
    {
        // Ensure the number of UI elements matches the number of cards on the table 
        // Only 3 cards on table for now
        if (cardsOnTable.Count != cardsOnTableUI.Count) return;
        for (int i = 0; i < cardsOnTableUI.Count; i++)
        {
            cardsOnTableUI[i].SetupCardUI(cardsOnTable[i]);
        }
    }

    [Button("Show Guessing")]
    public void ShowGuessing()
    {
        if (panelVisible)
        {
            CanvasGroup cg = guessingpanel.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = guessingpanel.AddComponent<CanvasGroup>();
            }

            cg.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete(() => guessingpanel.SetActive(false));

            panelVisible = false;
        }
        else
        {
            if (guessingpanel == null)
            {
                Debug.Log("si llegue");
                return;
            }

            guessingpanel.SetActive(true);
            CanvasGroup cg = guessingpanel.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = guessingpanel.AddComponent<CanvasGroup>();
            }

            cg.alpha = 0f;
            cg.DOFade(1f, fadeDuration).SetEase(Ease.OutCubic);

            for (int i = 0; i < ButtonChoice.Length; i++)
            {
                RectTransform btnRect = ButtonChoice[i].GetComponent<RectTransform>();
                Vector2 startPos = btnRect.anchoredPosition;
                btnRect.anchoredPosition = new Vector2(startPos.x, startPos.y - 100f);

                btnRect.DOAnchorPosY(startPos.y, 0.3f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(i * buttonDelay);
            }

            panelVisible = true;
            cardMayorButton.SetData(cardMayorData);

        }

    }

    private void CreateMayorButtons()
    {
        List<CardRuntime> MayorCards = new List<CardRuntime>();
        MayorCards = CardsManager.Instance.GetMayorCardsLocked();
    }

}
