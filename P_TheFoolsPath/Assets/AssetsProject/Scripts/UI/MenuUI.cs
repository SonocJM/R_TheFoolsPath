using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UIWindow
{
    [Header("Buttons")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _inventoryButton;

    private StartUI _startUI;
    private SettingsUI _settingsUI;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    public override void Initialize()
    {
        Hide(true);
        _backButton.onClick.AddListener(_startUI.ShowStartUI);
    }
    [Button]
    public override void Show(bool instant = false)
    {
        gameObject.SetActive(true);

        if (instant)
        {
            canvasGroup.alpha = 1f;
        }
        else
        {
            canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.Linear);
        }
    }
    [Button]
    public override void Hide(bool instant = false)
    {
        if (instant)
        {
            canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
        }
    }

    public void ShowMenuUI()
    {
        Show();
    }
}
