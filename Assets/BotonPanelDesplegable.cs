using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonPanelDesplegable : MonoBehaviour
{
    public GameObject panelDesplegable;
    //public Image texturaBotonOcultar;
    //public Image texturaBotonDesplegar;
    public Animator animacionTransicion;
    private bool desplegado = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DesplegarOcultarPanel()
    {
        /*
        float posicionX = panelDesplegable.GetComponent<RectTransform>().position.x;
        float posicionY = panelDesplegable.GetComponent<RectTransform>().localPosition.y;

        if (!desplegado)
        {
            panelDesplegable.GetComponent<RectTransform>().localPosition = new Vector3(0, posicionY - 207, 0f);
            GetComponent<Image>().sprite = texturaBotonOcultar.sprite;
        }
        else
        {
            panelDesplegable.GetComponent<RectTransform>().localPosition = new Vector3(0f, posicionY + 207f, 0f);
            GetComponent<Image>().sprite = texturaBotonDesplegar.sprite;
        }
        desplegado = !desplegado;
        */
        if (name.Equals("BotonCerrarPanelCentral"))
        {
            StartCoroutine(OcultarPanel("desafio_panel_central_out"));
        }
        else
        {
            StartCoroutine(OcultarPanel("desafio_panel_desplegable_ocultar"));
        }
    }

    IEnumerator OcultarPanel(string animacion)
    {
        panelDesplegable.GetComponent<Animator>().Play(animacion);
        yield return new WaitForSeconds(0.5f);
        panelDesplegable.SetActive(false);

    }
}
