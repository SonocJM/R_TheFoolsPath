using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipInformation : MonoBehaviour
{
    public GameObject ToolTip;
    public TextMeshProUGUI textDescription;

    void Start()
    {
        ToolTip.SetActive(false);
    }
    public void ShowToolTip()
    {
        ToolTip.SetActive(true);
        CardRuntime cardRuntime = gameObject.GetComponent<CardUI>().CardRuntime;

        if (cardRuntime != null)
        {
            textDescription.text = cardRuntime.CardData_SO.Description;
        }
    }

    public void HideToolTip()
    {
        ToolTip.SetActive(false);
    }
}
