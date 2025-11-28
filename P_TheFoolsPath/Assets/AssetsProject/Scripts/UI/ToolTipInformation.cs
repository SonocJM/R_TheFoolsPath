using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipInformation : MonoBehaviour
{
    public TextMeshProUGUI textDescription;

    public void ShowToolTip()
    {
        CardRuntime cardRuntime = gameObject.GetComponent<CardUI>().CardRuntime;

        if (cardRuntime != null)
        {
            textDescription.text = cardRuntime.CardData_SO.Description;
        }
    }
}
