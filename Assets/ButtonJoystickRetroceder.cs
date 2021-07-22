using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonJoystickRetroceder : MonoBehaviour
{
    public GameObject botonModoSecuencia;
    public GameObject bluetooth;
    private bool cambioAvanzar = false;
    private bool cambioRetroceder = false;
    private bool cambioGirarIzquierda = false;
    private bool cambioGirarDerecha = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !botonModoSecuencia.GetComponent<BotonCambioModo>().EnModoSecuencia())
        {
            bluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString("ml,parar;;;");
            if (gameObject.transform.name.Equals("BotonAdelante"))
            {
                cambioAvanzar = !cambioAvanzar;
            }
            else if (gameObject.transform.name.Equals("BotonAtras"))
            {
                cambioRetroceder = !cambioRetroceder;
            }
            else if (gameObject.transform.name.Equals("BotonGirarIzquierda"))
            {
                cambioGirarIzquierda = !cambioGirarIzquierda;
            }
            else if (gameObject.transform.name.Equals("BotonGirarDerecha"))
            {
                cambioGirarDerecha = !cambioGirarDerecha;
            }
        }
        else if (Input.GetMouseButtonDown(0) && !botonModoSecuencia.GetComponent<BotonCambioModo>().EnModoSecuencia())
        {
            if (gameObject.transform.name.Equals("BotonAdelante") && !cambioAvanzar)
            {
                AccionARealizarML("avanzar");
                cambioAvanzar = !cambioAvanzar;
            }
            else if (gameObject.transform.name.Equals("BotonAtras") && !cambioRetroceder)
            {
                AccionARealizarML("retroceder");
                cambioRetroceder = !cambioRetroceder;
            }
            else if (gameObject.transform.name.Equals("BotonGirarIzquierda") && !cambioGirarIzquierda)
            {
                AccionARealizarML("girarIzquierda");
                cambioGirarIzquierda = !cambioGirarIzquierda;
            }
            else if (gameObject.transform.name.Equals("BotonGirarDerecha") && !cambioGirarDerecha)
            {
                AccionARealizarML("girarDerecha");
                cambioGirarDerecha = !cambioGirarDerecha;
            }
        }
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
