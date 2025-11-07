using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardMayorButton : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _iconImage;       // Sprite decorativo o ícono
    [SerializeField] private TMP_Text _cardNameText; // Nombre del arcano
    [SerializeField] private Button _button;

    private CardRuntime _cardRuntime;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        if (_button != null)
            _button.onClick.AddListener(OnButtonClicked);
    }

    /// <summary>
    /// Configura el botón con los datos del Arcano Mayor.
    /// </summary>
    public void SetupButton(CardRuntime cardRuntime)
    {
        _cardRuntime = cardRuntime;

        if (_cardRuntime == null || _cardRuntime.CardData_SO == null)
        {
            Debug.LogWarning($"{name} no tiene datos asignados.");
            return;
        }

        // Asignar sprite decorativo si es un Arcano Mayor
        if (_iconImage != null && _cardRuntime.CardData_SO is CardMayor_SO mayorSO)
        {
            if (mayorSO.SpriteAdivination != null)
                _iconImage.sprite = mayorSO.SpriteAdivination;
            else
                Debug.LogWarning($"El Arcano Mayor {_cardRuntime.CardName} no tiene ButtonSprite asignado.");
        }

        // Asignar nombre
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

        // Aquí puedes agregar lógica de selección o desbloqueo
        // Por ejemplo:
        // CardsManager.Instance.SetCardAsUnlocked(_cardRuntime.Id);
    }
}