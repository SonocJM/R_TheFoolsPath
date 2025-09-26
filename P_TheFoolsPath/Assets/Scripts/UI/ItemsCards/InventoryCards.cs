using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCards : UIWindow
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetInfo(Sprite sprite, string text)
    {
        if (sprite != null) _image.sprite = sprite;
        _text.text = text;
    }
}
