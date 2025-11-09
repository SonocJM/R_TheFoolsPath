using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : UIWindow
{
    [Header("Buttons")]
    [SerializeField] private Button _StartButton;

    private MenuUI _menuUI;

    [Header("Initial Screen UI")]
    [SerializeField] private Image _imageButton;
    [SerializeField] private Image _imageTitle;
    [SerializeField] private Image _image1;
    [SerializeField] private Image _image2;

    public int yDistanceImage1;
    public int yDistanceImage2;
    public float fadeDuration = 0.5f;

    public override void Initialize()
    {
        if (_StartButton != null)
        {
            _StartButton.onClick.AddListener(ShowMenuUI);   
        }
    }
    [Button]
    public override void Show(bool instant = false)
    {
        WindowCanvas.gameObject.SetActive(true);

        if (instant)
        {
            _image1.rectTransform.DOAnchorPosY(0, 0);
            _image2.rectTransform.DOAnchorPosY(0, 0);
            _imageButton.DOFade(1f, 0);
            _imageTitle.DOFade(1f, 0);
        }
        else
        {
            _image1.rectTransform.DOAnchorPosY(0, AnimationTime).SetEase(Ease.OutCubic);
            _image2.rectTransform.DOAnchorPosY(0, AnimationTime).SetEase(Ease.OutCubic);

            _imageButton.DOFade(1f, fadeDuration).SetEase(Ease.Linear);
            _imageTitle.DOFade(1f, fadeDuration).SetEase(Ease.Linear);
        }
    }
    [Button]
    public override void Hide(bool instant = false)
    {

        if (instant)
        {
            _image1.rectTransform.DOAnchorPosY(yDistanceImage1, 0);
            _image2.rectTransform.DOAnchorPosY(yDistanceImage2, 0);
            _imageButton.DOFade(0f, 0);
            _imageTitle.DOFade(0f, 0);
            WindowCanvas.gameObject.SetActive(false);
        }
        else
        {
            _image1.rectTransform.DOAnchorPosY(yDistanceImage1, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => WindowCanvas.gameObject.SetActive(false));

            _image2.rectTransform.DOAnchorPosY(yDistanceImage2, AnimationTime).SetEase(Ease.InCubic).OnComplete(() => WindowCanvas.gameObject.SetActive(false));

            _imageButton.DOFade(0f, fadeDuration).SetEase(Ease.Linear);

            _imageTitle.DOFade(0f, fadeDuration).SetEase(Ease.Linear);
        }
    }
    
    public void ShowMenuUI()
    {
        UIManager.Instance.HideUI(WindowsIDs.Start);
        UIManager.Instance.ShowUI(WindowsIDs.Menu);
        Debug.Log("ya me voy, menu");
    }
}
