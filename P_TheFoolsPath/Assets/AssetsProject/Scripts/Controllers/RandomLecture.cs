using UnityEngine;
using UnityEngine.UI;

public class RandomLecture : MonoBehaviour
{
    public float pity = 0.25f;

    public Button botonUI;

    void Start()
    {
        if (botonUI != null)
            botonUI.onClick.AddListener(AlHacerClick);
    }

    void AlHacerClick()
    {
        float randomValue = Random.Range(0, 1);

        Debug.Log("Valor aleatorio: " + randomValue);

        if (randomValue <= pity)
        {
            ActivarMetodo();
        }
        else
        {
            Debug.Log("No ocurrió el evento esta vez.");
        }
    }

    void ActivarMetodo()
    {
        Debug.Log("¡Evento activado!");
    }
}

