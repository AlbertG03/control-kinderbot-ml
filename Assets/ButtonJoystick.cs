using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonJoystick : MonoBehaviour
{
    public GameObject botonModoSecuencia;
    public GameObject bluetooth;
    private bool cambioAvanzar = false;
    private bool cambioRetroceder = false;
    private bool cambioGirarIzquierda = false;
    private bool cambioGirarDerecha = false;

    public GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    public GameObject objetoSeleccionado;
    private string mSAvanzar = "#s,a,100;;;#fin;;;";
    private string mSRetroceder = "#s,r,100;;;#fin;;;";
    private string mSGirarDerecha = "#s,d,100;;;#fin;;;";
    private string mSGirarIzquierda = "#s,i,100;;;#fin;;;";
    private string mSPausa = "#s,p;;;#fin;;;";
    private string mLAvanzar = "#mj,ml,avanzar;;;#fin;;;";
    private string mLRetroceder = "#mj,ml,retroceder;;;#fin;;;";
    private string mLGirarDerecha = "#mj,ml,girarDerecha;;;#fin;;;";
    private string mLGirarIzquierda = "#mj,ml,girarIzquierda;;;#fin;;;";
    private string mJAvanzar = "#bt;;;#mj,ml,avanzar;;;#fin;;;";
    private string mJRetroceder = "#bt;;;#mj,ml,retroceder;;;#fin;;;";
    private string mJGirarDerecha = "#bt;;;#mj,ml,girarDerecha;;;#fin;;;";
    private string mJGirarIzquierda = "#bt;;;#mj,ml,girarIzquierda;;;#fin;;;";
    private string mJParar = "#bt;;;#mj,ml,parar;;;#fin;;;";
    private string final = "#fin;;;";
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
                    else
                    {
                        if (objetoSeleccionado.gameObject.transform.name.Equals("BotonAdelante"))
                        {
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mSAvanzar);
                        }
                        else if (objetoSeleccionado.gameObject.transform.name.Equals("BotonAtras"))
                        {
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mSRetroceder);

                        }
                        else if (objetoSeleccionado.gameObject.transform.name.Equals("BotonGirarIzquierda"))
                        {
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mSGirarIzquierda);

                        }
                        else if (objetoSeleccionado.gameObject.transform.name.Equals("BotonGirarDerecha"))
                        {
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mSGirarDerecha);

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
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJParar);
                        }
                        else if (resultado.gameObject.transform.name.Equals("BotonAtras"))
                        {
                            cambioRetroceder = !cambioRetroceder;
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJParar);
                        }
                        else if (resultado.gameObject.transform.name.Equals("BotonGirarIzquierda"))
                        {
                            cambioGirarIzquierda = !cambioGirarIzquierda;
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJParar);
                        }
                        else if (resultado.gameObject.transform.name.Equals("BotonGirarDerecha"))
                        {
                            cambioGirarDerecha = !cambioGirarDerecha;
                            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJParar);
                        }
                    }
                }

            }
            objetoSeleccionado = null;

        }
        if (raycastResults != null)
            raycastResults.Clear();
    }

    

    public void AccionARealizarML(string accion)
    {

        if (!botonModoSecuencia.GetComponent<BotonCambioModo>().EnModoSecuencia())
        {
            if (accion.Equals("avanzar"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJAvanzar);
            }
            else if (accion.Equals("retroceder"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJRetroceder);

            }
            else if (accion.Equals("girarIzquierda"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJGirarIzquierda);

            }
            else if (accion.Equals("girarDerecha"))
            {
                bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(mJGirarDerecha);

            }
        }
    }
}
