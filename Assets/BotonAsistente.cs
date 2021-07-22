using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonAsistente : MonoBehaviour
{
    private Color colorDesactivado;
    private Color colorActivado;
    public GameObject listaDeAlfombras;
    // Start is called before the first frame update
    void Start()
    {
        colorActivado = new Color(1,1,1,1);
        colorDesactivado = new Color(1,1,1,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (listaDeAlfombras.GetComponent<ListaAlfombras>().AyudaActivada())
        {
            GetComponent<Image>().color = colorActivado;
        }
        else
        {
            GetComponent<Image>().color = colorDesactivado;
        }
    }
}
