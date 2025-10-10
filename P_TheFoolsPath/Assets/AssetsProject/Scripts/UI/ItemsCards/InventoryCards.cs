using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCards : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetInfo(Sprite sprite)
    {
        if (sprite != null) _image.sprite = sprite;
    }
}
