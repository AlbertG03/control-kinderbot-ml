using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnComandos : MonoBehaviour
{
    public GameObject panelSecuencia;
    public GameObject robot;
    public GameObject flecha;
    public GameObject botonDeModo;
    public GameObject botonIniciar;
    public GameObject botonBorrar;
    public GameObject botonDeshacer;
    public GameObject botonPausa;
    public GameObject listaDeSecuencia;
    public GameObject scroll;
    public GameObject panelAlfombras;
    public GameObject conexionBluetooth;
    private string texto_parar_borrar = Char.ConvertFromUtf32(7);
    private bool modoLibre;
    // Start is called before the first frame update
    void Start()
    {
        // Se comienza en modo libre.
        modoLibre = true;
        if (name.Equals("BotonModoLibre"))
        {

            //panelAlfombras.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
            //panelAlfombras.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0f);
            panelAlfombras.GetComponent<RectTransform>().offsetMin = new Vector2(
                panelAlfombras.GetComponent<RectTransform>().offsetMin.x, 0f);
            //panelAlfombras.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AgregarFlecha()
    {
        if (!botonDeModo.GetComponent<BtnComandos>().GetModoJoystick())
        {
            string texto_inicio_ml = "#inicio,ml;;;";
            string texto_fin = "#fin;;;";
            string texto_emitir_sonido = "#sonido;;;";
            panelSecuencia.GetComponent<GridLayoutGroup>().cellSize = new Vector2(45f, 45f);
            GameObject flecha = Instantiate(this.flecha);
            flecha.name = flecha.name.Substring(0, flecha.name.Length - 7);
            flecha.SetActive(true);
            flecha.transform.SetParent(panelSecuencia.transform);
            flecha.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            string textoAEnviar = texto_inicio_ml + texto_emitir_sonido + texto_fin;
            conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
        }

    }



    public void limpiarSecuencia()
    {
        int cantidadHijos = panelSecuencia.GetComponent<GridLayoutGroup>().transform.childCount;

        // Revisar el tag que tiene para saber qué es lo que tiene que hacer el botón cancelar
        if (gameObject.name.Equals("BotonCancelar"))
        {

            for (int i = 0; i < cantidadHijos; i++)
            {
                Destroy(panelSecuencia.GetComponent<GridLayoutGroup>().transform.GetChild(i).gameObject);
            }
            conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_parar_borrar);
        }
        else
        {
            pararRobot();
        }

    }

    public void pararRobot()
    {
        robot.GetComponent<RobotMover>().PararYa();
    }
    /**
     * La función CambirDeModo se utiliza para activar o desactivar todos los componentes para iniciar
     * el modo secuencia o modo libre.
     */
    public void CambiarDeModo()
    {
        // Se usa para saber si está o no activado el modo secuencia o libre.
        Color colorModo;

        if (botonDeModo.GetComponent<Image>().color.a == 1f)
        {
            colorModo = new Color(255, 255, 255, 0.5f);

            // Activar la barra de secuencia
            listaDeSecuencia.SetActive(true);

            // Activar los botones para iniciar el modo secuencia
            botonBorrar.SetActive(true);
            botonIniciar.SetActive(true);
            botonDeshacer.SetActive(true);
            botonPausa.SetActive(true);

            // Activar la interactividad del botón
            botonIniciar.GetComponent<Button>().interactable = true;
            botonBorrar.GetComponent<Button>().interactable = true;
            botonDeshacer.GetComponent<Button>().interactable = true;

            // Hacer aparecer la barra de secuencia
            panelAlfombras.GetComponent<RectTransform>().offsetMin =
                new Vector2(panelAlfombras.GetComponent<RectTransform>().offsetMin.x, 77.1f);

            // Cambiar el modo del robot al modo secuencia
            robot.GetComponent<RobotMover>().CambiarModoFrecuencia(true);

            panelAlfombras.GetComponent<ListaAlfombras>().ConseguirGrillaActiva().GetComponent<Secuencia>().DibujarCamino(true);
        }
        else
        {
            colorModo = new Color(255, 255, 255, 1f);

            // Limpiar y ocultar la barra de secuencia
            limpiarSecuencia();
            listaDeSecuencia.SetActive(false);

            // Desactivar los botones para iniciar el modo libre
            botonIniciar.SetActive(false);
            botonBorrar.SetActive(false);
            botonDeshacer.SetActive(false);
            botonPausa.SetActive(false);

            // Desactivar la interactividad del botón
            botonIniciar.GetComponent<Button>().interactable = false;
            botonBorrar.GetComponent<Button>().interactable = false;
            botonDeshacer.GetComponent<Button>().interactable = false;

            // Se hace desaparecer la barra de secuencia
            panelAlfombras.GetComponent<RectTransform>().offsetMin =
                new Vector2(panelAlfombras.GetComponent<RectTransform>().offsetMin.x, 0f);

            // Cambiar el modo del robot al modo libre
            robot.GetComponent<RobotMover>().CambiarModoFrecuencia(false);

            panelAlfombras.GetComponent<ListaAlfombras>().ConseguirGrillaActiva().GetComponent<Secuencia>().BorrarCasillasActivadas();
        }

        // Se cambia internamente si está, o no, en modo libre
        modoLibre = !modoLibre;

        // Se cambia el color que es con lo cuál se comprueba en qué modo está
        // HAY QUE CAMBIARLO.
        botonDeModo.GetComponent<Image>().color = colorModo;
    }

    public bool GetModoJoystick()
    {
        return modoLibre;
    }

    public void DeshacerUltimaAccion()
    {
        int cantidadHijos = panelSecuencia.transform.childCount;

        GameObject.Destroy(panelSecuencia.transform.GetChild(cantidadHijos - 1).gameObject);
    }
}
