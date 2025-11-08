using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : UIWindow
{
    [Header ("Buttons")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _guessingButton;
    [SerializeField] private Button _shuffleButton;

    private MenuUI _menuUI;
    private SettingsUI _settingsUI;

    [Header("Cards:")]
    [SerializeField] private Image _card1;
    [SerializeField] private Image _card2;
    [SerializeField] private Image _card3;
    [SerializeField] private Image _card4;

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
    [SerializeField] private GameObject mayorCardPrefab;   
    [SerializeField] private Transform mayorContent;  

    private List<CardMayorButton> mayorButtons = new List<CardMayorButton>();
    private bool panelVisible = false;

    private Image[] _cards;

    [Header("Cards Table UI")]
    [SerializeField] private List<CardUI> cardsOnTableUI = new List<CardUI>();

    public override void Initialize()
    {
        Hide(true);
        if (guessingpanel != null)
            guessingpanel.SetActive(false);

        _backButton.onClick.AddListener(HideGameplayUI);
        _backButton.onClick.AddListener(_menuUI.ShowMenuUI);
        _settingsButton.onClick.AddListener(_settingsUI.ShowSettingsUI);
        _shuffleButton.onClick.AddListener(ShuffleCards);
        _guessingButton.onClick.AddListener(ShowGuessing);

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
            _card1.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic);
            _card2.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic);
            _card3.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic);
            _card4.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic);
            canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete(() => WindowCanvas.gameObject.SetActive(false));
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

        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOAnchorPosY(upTargetY, moveDuration).SetEase(Ease.OutQuad))
           .Append(rect.DOAnchorPosX(rightTargetX, moveDuration).SetEase(Ease.OutQuad))
           .AppendCallback(() => activeCard.transform.SetAsFirstSibling())
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
        if (cardsOnTable.Count != cardsOnTableUI.Count) return;
        for (int i = 0; i < cardsOnTableUI.Count; i++)
        {
            cardsOnTableUI[i].SetupCardUI(cardsOnTable[i]);
        }
    }

    [Button("Show Guessing")]
    public void ShowGuessing()
    {
        if (guessingpanel == null)
        {
            Debug.LogWarning("GuessingPanel no asignado.");
            return;
        }

        if (panelVisible)
        {
            CanvasGroup cg = guessingpanel.GetComponent<CanvasGroup>();
            if (cg == null) cg = guessingpanel.AddComponent<CanvasGroup>();
            cg.DOFade(0f, fadeDuration).OnComplete(() => guessingpanel.SetActive(false));
            panelVisible = false;
        }
        else
        {
            guessingpanel.SetActive(true);

            CanvasGroup cg = guessingpanel.GetComponent<CanvasGroup>();
            if (cg == null) cg = guessingpanel.AddComponent<CanvasGroup>();

            cg.alpha = 0f;
            cg.DOFade(1f, fadeDuration).SetEase(Ease.OutCubic);

            CreateMayorButtons(CardsManager.Instance.GetMayorCardsLocked());

            panelVisible = true;
        }
    }

    public void CreateMayorButtons(List<CardRuntime> mayorCardsLocked)
    {
        foreach (Transform child in mayorContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var cardRuntime in mayorCardsLocked)
        {
            GameObject go = Instantiate(mayorCardPrefab, mayorContent);
            CardMayorButton mayorButton = go.GetComponent<CardMayorButton>();

            if (mayorButton != null)
            {
                mayorButton.SetupButton(cardRuntime);
                mayorButtons.Add(mayorButton);

                mayorButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    CardMayor_SO cardData = cardRuntime.CardData_SO as CardMayor_SO;
                    if (cardData != null)
                        OnMayorCardGuessed(cardData);
                });
            }
        }
    }

    public void OnMayorCardGuessed(CardMayor_SO selectedCard)
    {
        Debug.Log($"El jugador eligió el Arcano Mayor: {selectedCard.CardName}");

        bool acierto = CheckIfCorrectMayor(selectedCard);

        if (acierto)
        {
            Debug.Log($"¡Correcto! Se desbloqueó {selectedCard.CardName}");
            CardRuntime cardToUnlock = CardsManager.Instance.CardsInventory.Find(c => c.CardData_SO == selectedCard);
            if (cardToUnlock != null)
                CardsManager.Instance.SetCardAsUnlocked(cardToUnlock.Id);
        }
        else
        {
            Debug.Log($"Fallaste. {selectedCard.CardName} no era el Arcano correcto.");
        }

        CanvasGroup cg = guessingpanel.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.DOFade(0f, fadeDuration).OnComplete(() => guessingpanel.SetActive(false));
        }

        panelVisible = false;
    }

    private bool CheckIfCorrectMayor(CardMayor_SO selectedCard)
    {
        return Random.value > 0.5f;
    }

    public void HideGameplayUI()
    {
        Hide();
    }
}

