using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonJoystickGirarDerecha : MonoBehaviour
{
    public GameObject botonModoSecuencia;
    public GameObject bluetooth;
    //private bool sePuedeCambiar = true;
    private bool cambioAvanzar = false;
    private bool cambioRetroceder = false;
    private bool cambioGirarIzquierda = false;
    private bool cambioGirarDerecha = false;

    public GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    public GameObject objetoSeleccionado;
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
                if (raycastResults[0].gameObject.tag.Equals("BotonJoystick"))
                {
                    Debug.Log("Pasó por acá :-)");
                    objetoSeleccionado = raycastResults[0].gameObject;
                    if (!botonModoSecuencia.GetComponent<BotonCambioModo>().EnModoSecuencia())
                    {
                        if (objetoSeleccionado.gameObject.transform.name.Equals("BotonAdelante") && !cambioAvanzar)
                        {
                            AccionARealizarML("avanzar");
                            cambioAvanzar = !cambioAvanzar;
                        }
                        else if (objetoSeleccionado.gameObject.transform.name.Equals("BotonAtras") && !cambioRetroceder)
                        {
                            AccionARealizarML("retroceder");
                            cambioRetroceder = !cambioRetroceder;
                        }
                        else if (objetoSeleccionado.gameObject.transform.name.Equals("BotonGirarIzquierda") && !cambioGirarIzquierda)
                        {
                            AccionARealizarML("girarIzquierda");
                            cambioGirarIzquierda = !cambioGirarIzquierda;
                        }
                        else if (objetoSeleccionado.gameObject.transform.name.Equals("BotonGirarDerecha") && !cambioGirarDerecha)
                        {
                            AccionARealizarML("girarDerecha");
                            cambioGirarDerecha = !cambioGirarDerecha;
                        }
                    }
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

                    if (!botonModoSecuencia.GetComponent<BotonCambioModo>().EnModoSecuencia())
                    {
                        if (resultado.gameObject.transform.name.Equals("BotonAdelante"))
                        {
                            cambioAvanzar = !cambioAvanzar;
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml_parar;;;");
                        }
                        else if (resultado.gameObject.transform.name.Equals("BotonAtras"))
                        {
                            cambioRetroceder = !cambioRetroceder;
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml_parar;;;");
                        }
                        else if (resultado.gameObject.transform.name.Equals("BotonGirarIzquierda"))
                        {
                            cambioGirarIzquierda = !cambioGirarIzquierda;
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml_parar;;;");
                        }
                        else if (resultado.gameObject.transform.name.Equals("BotonGirarDerecha"))
                        {
                            cambioGirarDerecha = !cambioGirarDerecha;
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml,parar;;;");
                        }
                    }
                }

            }
            objetoSeleccionado = null;

        }
        if (raycastResults != null)
            raycastResults.Clear();
    }

    public void AccionARealizar(string accion)
    {

        if (botonModoSecuencia.GetComponent<BotonCambioModo>().EnModoSecuencia())
        {
            if (accion.Equals("avanzar"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("avanzar,1,100;;;");
            }
            else if (accion.Equals("retroceder"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("retroceder,1,100;;;");

            }
            else if (accion.Equals("girarIzquierda"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("girarIzquierda,90,100;;;");

            }
            else if (accion.Equals("girarDerecha"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("girarDerecha,90,100;;;");

            }
        }
    }

    public void AccionARealizarML(string accion)
    {

        if (!botonModoSecuencia.GetComponent<BotonCambioModo>().EnModoSecuencia())
        {
            if (accion.Equals("avanzar"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml,avanzar,100;;;");
            }
            else if (accion.Equals("retroceder"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml,retroceder,100;;;");

            }
            else if (accion.Equals("girarIzquierda"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml,girarIzquierda,100;;;");

            }
            else if (accion.Equals("girarDerecha"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml,girarDerecha,100;;;");

            }
        }
    }
}
