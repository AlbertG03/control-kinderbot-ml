using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SiguienteSlide(string orientacion)
    {
        int posicionActual = 0;
        if (orientacion.Equals("derecha"))
        {
            posicionActual = BuscarSlideActivado();
            if (posicionActual != transform.childCount - 1 )
            {
                transform.GetChild(posicionActual).gameObject.SetActive(false);
                transform.GetChild(posicionActual + 1).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(posicionActual).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else if (orientacion.Equals("izquierda"))
        {
            posicionActual = BuscarSlideActivado();
            if (posicionActual != 0)
            {
                transform.GetChild(posicionActual).gameObject.SetActive(false);
                transform.GetChild(posicionActual-1).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(posicionActual).gameObject.SetActive(false);
                transform.GetChild(transform.childCount-1).gameObject.SetActive(true);
            }
        }
    }

    int BuscarSlideActivado()
    {
        int posicion = 0;
        Debug.Log(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                posicion = i;
            }
        }
        return posicion;
    }
}
