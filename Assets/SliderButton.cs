using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SliderButton : MonoBehaviour
{

    public GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    public GameObject objetoSeleccionado;
    public GameObject slider;
    public GameObject texto;
    // Start is called before the first frame update
    void Start()
    {
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointerEventData.position = Input.mousePosition;
            graphicRaycaster.Raycast(pointerEventData, raycastResults);
            if (raycastResults.Count > 0)
            {
                if (raycastResults[0].gameObject.tag.Equals("SliderButton"))
                {
                    Debug.Log("Pasó por acá :-)");
                    objetoSeleccionado = raycastResults[0].gameObject;
                    //
                    //objetoSeleccionado.transform.SetParent(canvas);
                }
            }
        }

        if (objetoSeleccionado != null && Input.GetMouseButtonUp(0))
        {
            pointerEventData.position = Input.mousePosition;
            raycastResults.Clear();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var resultado in raycastResults)
                {

                    if (resultado.gameObject.tag.Equals("SliderButton"))
                    {
                        objetoSeleccionado.transform.parent.transform.parent.GetComponent<Slider>().value = 50f;
                    }
                }

            }
            objetoSeleccionado = null;

        }
        if (raycastResults != null)
            raycastResults.Clear();
    }

    public void CambiarTexto()
    {
        texto.GetComponent<TextMeshProUGUI>().SetText(GetComponent<Slider>().value.ToString());
    }
}
