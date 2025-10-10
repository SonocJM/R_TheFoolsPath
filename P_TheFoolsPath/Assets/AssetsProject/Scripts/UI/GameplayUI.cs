using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : UIWindow
{
    [SerializeField] private Image _card1;
    [SerializeField] private Image _card2;
    [SerializeField] private Image _card3;
    [SerializeField] private Image _card4;
    [SerializeField] private Image _slider;

    public CanvasGroup canvasGroup;

    public int yDistanceCards;
    public int xDistanceSlider;
    public float fadeDuration = 0.5f;

    public override void Initialize()
    {
        Hide(true);
    }

    public override void Show(bool instant = false)
    {
        gameObject.SetActive(true);

        if (instant)
        {
            _card1.rectTransform.DOAnchorPosY(-511, 0);
            _card2.rectTransform.DOAnchorPosY(-570, 0);
            _card3.rectTransform.DOAnchorPosY(-651, 0);
            _card4.rectTransform.DOAnchorPosY(-687, 0);

            _slider.rectTransform.DOAnchorPosX(0, 0);

            canvasGroup.alpha = 1f;
        }
        else
        {
            _card1.rectTransform.DOAnchorPosY(-511, AnimationTime).SetEase(Ease.OutCubic);
            _card2.rectTransform.DOAnchorPosY(-570, AnimationTime).SetEase(Ease.OutCubic);
            _card3.rectTransform.DOAnchorPosY(-651, AnimationTime).SetEase(Ease.OutCubic);
            _card4.rectTransform.DOAnchorPosY(-687, AnimationTime).SetEase(Ease.OutCubic);

            _slider.rectTransform.DOAnchorPosX(400, AnimationTime).SetEase(Ease.OutCubic);

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

            _slider.rectTransform.DOAnchorPosX(xDistanceSlider, 0);

            canvasGroup.alpha = 0f;

            gameObject.SetActive(false);
        }
        else
        {
            _card1.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
            _card2.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
            _card3.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));
            _card4.rectTransform.DOAnchorPosY(yDistanceCards, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));

            _slider.rectTransform.DOAnchorPosX(xDistanceSlider, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => gameObject.SetActive(false));

            canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        }
    }
}
