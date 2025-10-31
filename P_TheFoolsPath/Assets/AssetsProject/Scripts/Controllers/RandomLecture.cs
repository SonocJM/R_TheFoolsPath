using UnityEngine;
using UnityEngine.UI;

public class RandomLecture : MonoBehaviour
{
    public float pity = 0.25f;

    public Button botonUI;

    CardsData_SO[] cardsMinor;

    void Start()
    {
        if (botonUI != null)
            botonUI.onClick.AddListener(clickLecture);
    }

    void clickLecture()
    {
        float randomValue = Random.Range(0, 1);

        Debug.Log("Valor aleatorio: " + randomValue);

        if (randomValue <= pity)
        {
            cardJump();
        }
        else
        {
            Debug.Log("No salio ninunga carta");
        }
 
        cardsMinor = CardsManager.Instance.SelectedMayorCard.CardMinorList;
    }

    void cardJump()
    {
        Debug.Log("salio carta");
        //For de index de lista de los minor
    }
}

