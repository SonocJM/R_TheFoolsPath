using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UIWindow
{
    [Header ("Buttons")]
    [SerializeField] private Button _backButton;
    
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    public override void Initialize()
    {
        Hide(true);
        if (_backButton != null)
        {
            _backButton.onClick.AddListener(ShowMenuUI);   
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

    public void ShowMenuUI()
    {
        UIManager.Instance.HideUI(WindowsIDs.Settings);
        UIManager.Instance.ShowUI(WindowsIDs.Menu);
    }
}
