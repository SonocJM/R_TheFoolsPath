using DG.Tweening;
using Unity.Collections;
using UnityEngine;
using NaughtyAttributes;

public class UIWindow : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _windowID;
    [SerializeField] private Canvas _windowCanvas;
    [SerializeField] private CanvasGroup _windowCanvasGroup;

    [Header("Options")]
    [SerializeField] private bool hideOnStart = true;
    [SerializeField] private float animationTime = 0.5f;

    [Header ("Animations Entry")]
    [SerializeField] private Ease easeShow = Ease.InBack;
    [SerializeField] private Ease easeHide = Ease.OutBack;

    public bool IsShowing { get; private set; } = false;
    public string WindowUI => _windowID;
    public float AnimationTime => animationTime;

    public void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        if (hideOnStart) Hide(instant: true);
    }

    public virtual void Show(bool instant = false)
    {
        if (IsShowing) return;

        _windowCanvas.gameObject.SetActive(true);

        if (instant)
        {
            _windowCanvasGroup.transform.DOScale(endValue: Vector3.one, duration: 0f);
        }
        else
        {
            _windowCanvasGroup.transform.DOScale(endValue: Vector3.one, animationTime);
        }
    }
    public virtual void Hide(bool instant = false)
    {
        //_windowCanvas.gameObject.SetActive(false);

        if (instant)
        {
            _windowCanvasGroup.transform.DOScale(endValue: Vector3.one, duration: 0f);
        }
        else
        {
            _windowCanvasGroup.transform.DOScale(endValue: Vector3.one, animationTime);
        }
    }
    private void DisableCanvas()
    {
        _windowCanvas.gameObject.SetActive(false);
        IsShowing = false;
    }


}
