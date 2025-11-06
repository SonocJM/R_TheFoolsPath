using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardMayorButton : MonoBehaviour
{
    [Header("Card Mayor Data")]
    [SerializeField] private CardMayor_SO cardMayorData;

    [Header("UI References")]
    [SerializeField] private Image cardImage;
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        //if (button != null)
        //    button.onClick.AddListener(OnButtonClick);
    }

    private void Start()
    {
        SetupButton();
    }

    public void SetupButton()
    {
        if (cardMayorData == null)
        {
            Debug.LogWarning($"{name} no tiene asignado un CardMayor_SO.");
            return;
        }

        if (cardImage != null)
            cardImage.sprite = cardMayorData.Sprite;

        if (cardNameText != null)
            cardNameText.text = cardMayorData.CardName;
    }

    //private void OnButtonClick()
    //{
    //    if (cardMayorData == null)
    //    {
    //        Debug.LogWarning("No hay carta mayor asignada.");
    //        return;
    //    }


    //    if (activeMinors == null || activeMinors.Count == 0)
    //    {
    //        Debug.Log("No hay cartas menores activas.");
    //        return;
    //    }

    //    if (CheckIfMatch(activeMinors))
    //    {
    //        Debug.Log($"Carta mayor desbloqueada: {cardMayorData.CardName}");
    //    }
    //    else
    //    {
    //        Debug.Log("Combinación incorrecta.");
    //    }
    //}

    //private bool CheckIfMatch(System.Collections.Generic.List<CardRuntime> activeMinors)
    //{
    //    var related = cardMayorData.RelatedMinorCards;

    //    int matchCount = 0;
    //    foreach (var minor in activeMinors)
    //    {
    //        foreach (var relatedMinor in related)
    //        {
    //            if (minor.CardData_SO == relatedMinor)
    //            {
    //                matchCount++;
    //                break;
    //            }
    //        }
    //    }

    //    return matchCount == related.Length;
    //}

    public void SetData(CardMayor_SO newInfo)
    {
        cardMayorData = newInfo;
        SetupButton();
    }
}

