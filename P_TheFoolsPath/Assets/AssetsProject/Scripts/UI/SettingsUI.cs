using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class SettingsUI : UIWindow
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

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

    public void ShowSettingsUI()
    {
        Show();
    }
}
