using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelImageSelector : MonoBehaviour
{
    public ListaAlfombras panel;
    public GameObject robot;
    public GameObject mensaje;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
       
    public void CambiarImagen(string direccion)
    {
        int posicionActivo = BuscarPosicionActivo(null), cantidadHijos = transform.childCount;

        transform.GetChild(posicionActivo).gameObject.SetActive(false);

        if (direccion.Equals("derecha"))
        {
            if (posicionActivo == cantidadHijos-1)
            {
                
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(posicionActivo + 1).gameObject.SetActive(true);
            }
        }
        else if (direccion.Equals("izquierda"))
        {
            if (posicionActivo == 0)
            {
                transform.GetChild(cantidadHijos - 1).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(posicionActivo - 1).gameObject.SetActive(true);
            }
        }

    }

    int BuscarPosicionActivo(ListaAlfombras listaAlfombras)
    {
        int posicionActivo = 0;
        if (listaAlfombras == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    posicionActivo = i;
                }
            }
        }
        else
        {
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                if (panel.transform.GetChild(i).gameObject.activeSelf)
                {
                    posicionActivo = i;
                }
            }
        }
        return posicionActivo;
    }

    public void ActivarAlfombra()
    {
        int posicionActivoSelector = BuscarPosicionActivo(null), posicionActivoAlfombra = BuscarPosicionActivo(panel);

        Debug.Log(posicionActivoAlfombra);
        if (!robot.GetComponent<RobotMover>().EstasEnCasita())
        {
            robot.GetComponent<RobotMover>().IrACasa();
        }
        //panel.transform.GetChild(posicionActivoAlfombra).gameObject.SetActive(false);

        /*if (posicionActivoSelector == 0)
        {
            panel.transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            panel.transform.GetChild(posicionActivoSelector - 1).gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(MostrarToast());
        }*/
    }

    IEnumerator MostrarToast()
    {
        mensaje.SetActive(true);
        yield return new WaitForSeconds(3);
        mensaje.SetActive(false);

    }
}
