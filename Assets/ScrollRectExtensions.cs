using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectExtensions : MonoBehaviour
{
    public GameObject panelDeSecuencia;
    private float anchoDelPanelDeSecuencia;
    private float tamanioDeLaBarra;
    void Start()
    {
        // Se guarda el valor del scroll para luego compararlo
        anchoDelPanelDeSecuencia = panelDeSecuencia.GetComponent<RectTransform>().rect.width;

        tamanioDeLaBarra = GetComponent<Scrollbar>().size;
    }

    void Update()
    {
        float tamanioActual = (float)System.Math.Round(GetComponent<Scrollbar>().size, 2);
        if (tamanioDeLaBarra != tamanioActual)
        {
            tamanioDeLaBarra = (float)System.Math.Round(GetComponent<Scrollbar>().size, 2);
            Debug.Log(tamanioActual);
            Debug.Log(tamanioDeLaBarra);
            GetComponent<Scrollbar>().value = 1;
        }
    }


}
