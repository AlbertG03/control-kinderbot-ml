using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListaAlfombras : MonoBehaviour
{
    private List<GameObject> listadoAlfombras;
    private int alfombraActiva = 0;
    // Start is called before the first frame update
    void Start()
    {
        listadoAlfombras = new List<GameObject>();
        int cantidadAlfombras = gameObject.transform.childCount;
        for (int i = 0; i < cantidadAlfombras; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<Alfombras>().activado)
            {
                alfombraActiva = i;
            }
        }
        //Debug.Log(alfombraActiva);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarSiguienteAlfombra()
    {
        
        int cantidadAlfombras = gameObject.transform.childCount;
        //Debug.Log(cantidadAlfombras);
        for (int i = 0; i < cantidadAlfombras; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<Alfombras>().activado)
            {
                alfombraActiva = i;
            }
            gameObject.transform.GetChild(i).GetComponent<Alfombras>().Desactivar();
        }
        //Debug.Log(alfombraActiva);
        if (alfombraActiva + 1 == transform.childCount)
        {
            
            gameObject.transform.GetChild(0).GetComponent<Alfombras>().Activar();
        }
        else
        {
            gameObject.transform.GetChild(alfombraActiva + 1).GetComponent<Alfombras>().Activar();
        }
    }

    public void ActivarSiguienteAtrasAlfombra()
    {
        int cantidadAlfombras = gameObject.transform.childCount;
        //Debug.Log(cantidadAlfombras);
        for (int i = 0; i < cantidadAlfombras; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<Alfombras>().activado)
            {
                alfombraActiva = i;
            }
            gameObject.transform.GetChild(alfombraActiva).GetComponent<Alfombras>().Desactivar();
        }
        //Debug.Log(alfombraActiva);
        if (alfombraActiva == 0)
        {
            gameObject.transform.GetChild(cantidadAlfombras - 1).GetComponent<Alfombras>().Activar();
        }
        else
        {
            gameObject.transform.GetChild(alfombraActiva - 1).GetComponent<Alfombras>().Activar();
        }
    }

    public GameObject ConseguirGrillaActiva()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                for (int f = 0; f < transform.GetChild(i).transform.childCount; f++)
                {
                    if (transform.GetChild(i).transform.GetChild(f).name.Equals("Grilla"))
                    {
                        return transform.GetChild(i).transform.GetChild(f).gameObject;
                    }
                }
            }
        }

        return null;
    }

    public void GestionarAyuda()
    {
        ConseguirGrillaActiva().GetComponent<Secuencia>().GestionarAyuda();
        //ConseguirGrillaActiva().GetComponent<Secuencia>().DibujarCamino(true);
    }

    public bool AyudaActivada()
    {
        return ConseguirGrillaActiva().GetComponent<Secuencia>().AyudaActivada();
         
    }
}
