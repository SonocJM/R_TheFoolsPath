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
    

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    public override void Initialize()
    {
        Hide(true);
        if (_backButton != null)
        {
            _backButton.onClick.AddListener(ShowStartUI);   
        }

        if (_settingsButton != null)
        {
            _settingsButton.onClick.AddListener(ShowSettingsUI);   
        }

        if (_playButton != null)
        {
            _playButton.onClick.AddListener(ShowGameplayUI);
        }

        if (_inventoryButton != null)
        {
            _inventoryButton.onClick.AddListener(ShowInventoryUI);
        }
    }
    [Button]
    public override void Show(bool instant = false)
    {
        WindowCanvas.gameObject.SetActive(true);

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
            WindowCanvas.gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.Linear).OnComplete(() => WindowCanvas.gameObject.SetActive(false));
        }
    }
    public void ShowGameplayUI()
    {
        UIManager.Instance.HideUI(WindowsIDs.Menu);
        UIManager.Instance.ShowUI(WindowsIDs.Gameplay);
    }
    
    public void ShowInventoryUI()
    {
        UIManager.Instance.HideUI(WindowsIDs.Menu);
        UIManager.Instance.ShowUI(WindowsIDs.Inventory);
    }
    public void ShowSettingsUI()
    {
        UIManager.Instance.HideUI(WindowsIDs.Menu);
        UIManager.Instance.ShowUI(WindowsIDs.Settings);
    }

    public void ShowStartUI()
    {
        UIManager.Instance.HideUI(WindowsIDs.Menu);
        UIManager.Instance.ShowUI(WindowsIDs.Start);
    }
    
    
}
