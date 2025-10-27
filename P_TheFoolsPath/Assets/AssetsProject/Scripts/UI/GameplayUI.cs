using DG.Tweening;
using NaughtyAttributes;
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

    private Image[] _cards;
    public override void Initialize()
    {
        Hide(true);
    }

    public override void Show(bool instant = false)
    {
        gameObject.SetActive(true);

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

    public override void Hide(bool instant = false)
    {

        if (instant)
        {
            _card1.rectTransform.DOAnchorPosY(yDistanceCards, 0);
            _card2.rectTransform.DOAnchorPosY(yDistanceCards, 0);
            _card3.rectTransform.DOAnchorPosY(yDistanceCards, 0);
            _card4.rectTransform.DOAnchorPosY(yDistanceCards, 0);

            canvasGroup.alpha = 0f;

            gameObject.SetActive(false);
        }
        else
        {
            _card1.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
            _card2.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
            _card3.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
            _card4.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));

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




}
