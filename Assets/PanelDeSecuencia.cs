/** Creador: Lucas Javier Caron
 *  Nombre de la clase: PanelDeSecuencia
 *  Descripción: Se encarga del manejo del espacio necesario para que pueda alojarse la casita
 *  y, también, de almacenar toda la secuencia de órdenes que se le van a dar al robot cuando
 *  se apriete sobre el botón iniciar.
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelDeSecuencia : MonoBehaviour
{
    public GraphicRaycaster graphicRaycaster;
    public Transform canvas;
    public GameObject listaDeSecuencia;
    public GameObject panelDeAlfombras;
    public GameObject objetoSeleccionado;
    public GameObject robot;
    public GameObject botonArriba;
    public GameObject botonAbajo;
    public GameObject botonIzquierda;
    public GameObject botonDerecha;
    public GameObject botonPausa;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    private GameObject copiaDeObjetoSeleccionado;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialización de las variables necesarias para poder capturar los objetos
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gestionar el espacio para poder ver la casita para alojar, o no, al robot
        GestionDeEspacioDeLaCasita();

        // Gestionar la eliminación de las ordenes.
        MovimientoDeFlechas();
    }



    /**
     * GestionDeEspacioDeLaCasita se encarga de crear el espacio necesario para que aparezca la casita
     */
    private void GestionDeEspacioDeLaCasita()
    {
        if (robot.GetComponent<RobotMover>().EstasEnCasita())
        {
            // Se hace visible el conjunto de elementos de la lista de secuencia
            listaDeSecuencia.SetActive(true);

            // Se amolda el contenedor de las alfombras para que se adapte a la pantalla
            panelDeAlfombras.GetComponent<RectTransform>().offsetMin =
                new Vector2(panelDeAlfombras.GetComponent<RectTransform>().offsetMin.x, 77.1f);
        }

    }

    /*
     * MovimientoDeFlechas se encarga de poder eliminar la flecha que se arrastre fuera del listado
     */
    private void MovimientoDeFlechas()
    {
        // Se inicia el proceso si se aprieta el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Capturar la posición del mouse
            pointerEventData.position = Input.mousePosition;

            // Capturar los objetos con los cuales existe un contacto
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            // Si existe un contacto, recorrer los elementos para poder saber si está lo que necesitamos
            if (raycastResults.Count > 0)
            {

                // Buscamos si es una flecha
                if (raycastResults[0].gameObject.tag == "FlechasDeSecuencia")
                {
                    // Capturamos el objeto que está seleccionado
                    objetoSeleccionado = raycastResults[0].gameObject;

                    // Guardamos el color del objeto capturado para luego poder reestablecerlo
                    color = objetoSeleccionado.GetComponent<RawImage>().color;

                    // Cambiamos el alpha del objeto seleccionado para que no se vea en la pantalla
                    objetoSeleccionado.GetComponent<RawImage>().color = new Color(0f, 0f, 0f, 0f);
                    //objetoSeleccionado.transform.SetParent(canvas);

                    // Vemos que tipo de flecha es para luego generar una copia que es la que vamos
                    // a arrastrar por la pantalla simulando que estamos arrastrando el objeto seleccionado
                    string nombreDeLaFlecha = objetoSeleccionado.name;
                    if (nombreDeLaFlecha.Equals("FlechaArriba"))
                    {
                        copiaDeObjetoSeleccionado = Instantiate(botonArriba);

                    }
                    else if (nombreDeLaFlecha.Equals("FlechaAbajo"))
                    {
                        copiaDeObjetoSeleccionado = Instantiate(botonAbajo);
                    }
                    else if (nombreDeLaFlecha.Equals("FlechaGirarIzquierda"))
                    {
                        copiaDeObjetoSeleccionado = Instantiate(botonIzquierda);
                    }
                    else if (nombreDeLaFlecha.Equals("FlechaGirarDerecha"))
                    {
                        copiaDeObjetoSeleccionado = Instantiate(botonDerecha);
                    }
                    else if (nombreDeLaFlecha.Equals("FlechaPausa"))
                    {
                        copiaDeObjetoSeleccionado = Instantiate(botonPausa);
                    }


                    // Procesamos la copia creada previamente
                    if (copiaDeObjetoSeleccionado != null)
                    {
                        // Le damos un padre que en este caso será canvas
                        copiaDeObjetoSeleccionado.transform.SetParent(canvas.transform);

                        // Le damos un tamaño
                        copiaDeObjetoSeleccionado.GetComponent<RectTransform>().sizeDelta = new Vector2(25f,25f);

                        // Lo hacemos visible
                        copiaDeObjetoSeleccionado.SetActive(true);
                    }

                }
            }
        }

        // Gestión de la posición de la copia del objeto seleccionado
        if (objetoSeleccionado != null)
        {
            // Asignar los valores de la posición del mouse a la copia del objeto seleccionado
            copiaDeObjetoSeleccionado.GetComponent<RectTransform>().localPosition = CanvasScreen(Input.mousePosition);
        }

        // Gestión cuando se deja de apretar el botón izquierdo del mouse
        if (objetoSeleccionado != null && Input.GetMouseButtonUp(0))
        {
            // Crear variable para saber si queda o no dentro del panel
            bool enPanel = false;

            // Capturar la posición del mouse
            pointerEventData.position = Input.mousePosition;

            // Limpieza de los rayos que me dicen la conexión con distintos objetos
            raycastResults.Clear();

            // Generar los rayos para saber las interacciones entre los objetos
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            // Se procesa los rayos
            if (raycastResults.Count > 0)
            {
                foreach (var resultado in raycastResults)
                {
                    // Si en los resultados existe una conexión con el panel de secuencia, el objeto seleccionado
                    // no se destruye pero la copia si.
                    if (resultado.gameObject.tag == "PanelDeSecuencia")
                    {
                        // Al estar todavía en el panel, se restaura el alpha a la flecha
                        objetoSeleccionado.GetComponent<RawImage>().color = color;

                        // Se destruye la copia del objeto seleccionado
                        Destroy(copiaDeObjetoSeleccionado);

                        // Se indica que efectivamente está en el panel
                        enPanel = true;
                    }

                }
            }

            // Si es que no está en el panel, se elimina el objeto seleccionado
            if (!enPanel && objetoSeleccionado != null)
            {
                // Destrucción del objeto seleccionado.
                Destroy(objetoSeleccionado);
            }

            // Se vacía completamente el objeto seleccionado para una nueva operación
            objetoSeleccionado = null;

            // Destrucción de la copia del objeto seleccionado
            Destroy(copiaDeObjetoSeleccionado);

        }

        // Limpieza de los rayos
        if (raycastResults != null)
            raycastResults.Clear();

    }

    /** Se utiliza para capturar la posición dentro de canvas, esto nos permite tener las coordenadas
     *  correctas en la pantalla
     */
    public Vector2 CanvasScreen(Vector2 screenPos)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPos);
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }
}
