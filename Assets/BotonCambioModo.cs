using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonCambioModo : MonoBehaviour
{
    public GameObject circulo;
    public GameObject libre;
    public GameObject secuencia;
    public GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    public Transform canvas;
    public GameObject objetoSeleccionado;
    public GameObject textoLibre;
    public GameObject textoFijo;

    private bool enModoSecuencia = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (circulo.GetComponent<RectTransform>().localPosition.x > -33f &&
           circulo.GetComponent<RectTransform>().localPosition.x < 33f)
        {
            float nuevaPosicion;
            if (enModoSecuencia)
            {
                nuevaPosicion = circulo.GetComponent<RectTransform>().localPosition.x - 5f;
                
            }
            else
            {
                nuevaPosicion = circulo.GetComponent<RectTransform>().localPosition.x + 5f;
            }
            circulo.GetComponent<RectTransform>().localPosition = new Vector3(nuevaPosicion, 0f, 0f);
        }
        else
        {
            if (enModoSecuencia)
            {
                circulo.GetComponent<RectTransform>().localPosition = new Vector3(-35f, 0f, 0f);
                textoFijo.SetActive(true);
                textoLibre.SetActive(false);
            }
            else
            {
                circulo.GetComponent<RectTransform>().localPosition = new Vector3(35f, 0f, 0f);
                textoFijo.SetActive(false);
                textoLibre.SetActive(true);
            }
        }
    }

    public void CambiarDeModo()
    {
        if (enModoSecuencia)
        {

            circulo.GetComponent<RectTransform>().localPosition = new Vector3(-32f, 0f, 0f);
            libre.SetActive(true);
            secuencia.SetActive(false);
        }
        else
        {
            circulo.GetComponent<RectTransform>().localPosition = new Vector3(32f, 0f, 0f);
            libre.SetActive(false);
            secuencia.SetActive(true);
        }
        enModoSecuencia = !enModoSecuencia;
    }

    public bool EnModoSecuencia()
    {
        return enModoSecuencia;
    }
}
