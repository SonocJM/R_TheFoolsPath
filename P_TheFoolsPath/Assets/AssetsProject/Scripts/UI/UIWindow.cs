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


    public string WindowUI => _windowID;

    public void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        if (hideOnStart) Hide(instant: true);
    }

    [Button]
    public virtual void Show(bool instant = false)
    {
        //_windowCanvas.gameObject.SetActive(true);
        if (instant)
        {
            _windowCanvasGroup.transform.DOScale(endValue: Vector3.one, duration: 0f);
        }
        else
        {
            _windowCanvasGroup.transform.DOScale(endValue: Vector3.one, animationTime);
        }
    }
    [Button]
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


}
