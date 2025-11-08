using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardMayorButton : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _iconImage;       
    [SerializeField] private TMP_Text _cardNameText;
    [SerializeField] private Button _button;

    private CardRuntime _cardRuntime;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        if (_button != null)
            _button.onClick.AddListener(OnButtonClicked);
    }
    public void SetupButton(CardRuntime cardRuntime)
    {
        _cardRuntime = cardRuntime;
        Debug.Log("Si llego");

        if (_cardRuntime == null || _cardRuntime.CardData_SO == null)
        {
            Debug.Log($"{name} no tiene datos asignados.");
            return;
        }

        if (_iconImage != null && _cardRuntime.CardData_SO is CardMayor_SO mayorSO)
        {
            if (mayorSO.SpriteAdivination != null)
                _iconImage.sprite = mayorSO.SpriteAdivination;
            else
                Debug.Log($"El Arcano Mayor {_cardRuntime.CardName} no tiene ButtonSprite asignado.");
        }

        if (_cardNameText != null)
            _cardNameText.text = _cardRuntime.CardName;
    }

    private void OnButtonClicked()
    {
        if (_cardRuntime == null)
        {
            Debug.LogWarning("No hay carta asignada al botón.");
            return;
        }

        Debug.Log($"Has seleccionado el Arcano Mayor: {_cardRuntime.CardName}");

    }
}