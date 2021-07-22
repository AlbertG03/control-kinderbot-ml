using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Secuencia : MonoBehaviour
{
    public GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    public Transform canvas;
    public GameObject objetoSeleccionado;
    public GameObject casita;
    public GameObject alfombra;
    public GameObject imagen;
    public GameObject listaDeSecuencia;
    public GameObject panelDeSecuencia;
    public GameObject panelDeAlfombras;
    public GameObject contenedorDeAlfombra;
    public int posicionCelda = 0;
    public GameObject robot;
    private Transform grillaPadre;
    private float anchoContenedor, altoContenedor;
    private int cantidadDeColumnas, cantidadDeOrdenes = 0, posicionAnterior = 0;
    private float anchoDeLaGrilla,
        anchoDeLaCelda,
        anchoDeLaFoto,
        altoDeLaFoto;
    private Color colorCeldaTransparente, colorCeldaCamino, colorCeldaError;
    private bool hayError = false, hayCambioEnLaLista = false, ayuda = false;
    private string direccionRobot = "arriba";


    // Start is called before the first frame update
    void Start()
    {
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
        // Conseguir la cantidad de columnas
        cantidadDeColumnas = GetComponent<GridLayoutGroup>().constraintCount;

        // Cargar el color de las casillas
        colorCeldaCamino = new Color(255f, 255f, 255f, 0.5f);
        colorCeldaError = new Color(1, 0, 0, 0.5f);
        colorCeldaTransparente = new Color(0f, 0f, 0f, 0f);


    }

    // Update is called once per frame
    void Update()
    {
        ArreglarGrilla();
        Arrastrar();
        DibujarCamino(false);
    }


    void ArreglarGrilla()
    {

        // Conseguir el ancho y alto del contenedor
        anchoDeLaFoto = imagen.GetComponent<RectTransform>().rect.width - (int)GetComponent<GridLayoutGroup>().padding.left * 2;
        altoDeLaFoto = imagen.GetComponent<RectTransform>().rect.height - (int)GetComponent<GridLayoutGroup>().padding.top * 2;

        // Cambiar el ancho y alto de la alfombra
        GetComponent<RectTransform>().sizeDelta = new Vector2(anchoDeLaFoto, altoDeLaFoto);

        // Conseguir el ancho de la grilla
        anchoDeLaGrilla = GetComponent<RectTransform>().rect.width - (int)GetComponent<GridLayoutGroup>().padding.left * 2;
        // Configurar el ancho de la celda
        anchoDeLaCelda = anchoDeLaGrilla / cantidadDeColumnas;

        // Modificar el ancho y alto de las celdas
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(anchoDeLaCelda, anchoDeLaCelda);
    }


    void Arrastrar()
    {

        if (Input.GetMouseButtonDown(0))
        {
            pointerEventData.position = Input.mousePosition;
            graphicRaycaster.Raycast(pointerEventData, raycastResults);
            if (raycastResults.Count > 0)
            {
                if (raycastResults[0].gameObject.GetComponent<RobotMover>())
                {
                    if (raycastResults[0].gameObject.GetComponent<RobotMover>().EsPosibleAgarrarlo())
                    {
                        objetoSeleccionado = raycastResults[0].gameObject;
                        //grillaPadre = objetoSeleccionado.transform.parent.transform;
                        objetoSeleccionado.transform.SetParent(canvas);
                        BorrarCasillasActivadas();
                    }
                    else
                    {
                        Debug.Log("No se puede seleccionar estando en movimiento");
                    }

                }
            }
        }

        if (objetoSeleccionado != null)
        {
            objetoSeleccionado.GetComponent<RectTransform>().localPosition = CanvasScreen(Input.mousePosition);
            casita.SetActive(true);
            // Hacer aparecer la barra de secuencia
            panelDeAlfombras.GetComponent<RectTransform>().offsetMin =
                new Vector2(panelDeAlfombras.GetComponent<RectTransform>().offsetMin.x, 77.1f);
        }

        if (objetoSeleccionado != null && Input.GetMouseButtonUp(0))
        {
            pointerEventData.position = Input.mousePosition;
            raycastResults.Clear();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                bool enCasilla = false;
                foreach (var resultado in raycastResults)
                {

                    if (resultado.gameObject.tag == "CeldaGrilla")
                    {
                        objetoSeleccionado.transform.SetParent(resultado.gameObject.transform);
                        objetoSeleccionado.transform.localPosition = Vector2.zero;
                        objetoSeleccionado.GetComponent<RectTransform>().sizeDelta = new Vector2(resultado.gameObject.GetComponent<RectTransform>().sizeDelta.x, resultado.gameObject.GetComponent<RectTransform>().sizeDelta.x);
                        enCasilla = true;
                        casita.SetActive(false);

                        // Se hace desaparecer la barra de secuencia
                        if (!objetoSeleccionado.GetComponent<RobotMover>().GetModo())
                        {
                            listaDeSecuencia.SetActive(false);
                            panelDeAlfombras.GetComponent<RectTransform>().offsetMin =
                                new Vector2(panelDeAlfombras.GetComponent<RectTransform>().offsetMin.x, 0f);
                        }
                        else
                        {
                            DibujarCamino(true);
                        }
                    }
                }
                if (!enCasilla)
                {
                    //casita.SetActive();
                    objetoSeleccionado.transform.SetParent(casita.transform);
                    objetoSeleccionado.transform.localPosition = Vector2.zero;
                    objetoSeleccionado.GetComponent<RectTransform>().sizeDelta = new Vector2(47, 47);
                    posicionCelda = 0;
                }
            }
            objetoSeleccionado = null;

        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
            {
                posicionCelda = i;
            }
        }

        if (raycastResults != null)
            raycastResults.Clear();
    }

    public Vector2 CanvasScreen(Vector2 screenPos)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPos);
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize / 2));
    }

    public void CambiarParent()
    {
        Debug.Log(GetComponent<GridLayoutGroup>().constraintCount);
    }

    public void DibujarCamino(bool forzarDibujado)
    {
        // Comprobar que esté en alguna casilla
        if (!robot.GetComponent<RobotMover>().EstasEnCasita() && ayuda)
        {

            // Fijarse si hay algún cambio para no estar haciendo siempre lo mismo
            if (HayCambiosEnLaLista() || forzarDibujado)
            {
                // Fijarse el listado de ordenes que tiene
                ArrayList listaDeOrdenes = new ArrayList();
                listaDeOrdenes = CargarListadoDeOrdenes();

                hayError = false;
                // Activar las casillas
                ActivarCasillas(listaDeOrdenes, colorCeldaCamino);

                // Si hay un error poner las casillas en rojo
                if (hayError)
                {

                    ActivarCasillas(listaDeOrdenes, colorCeldaError);
                    //hayError = false;
                }
            }
        }
    }

    private ArrayList CargarListadoDeOrdenes()
    {
        ArrayList listado = new ArrayList();

        for (int i = 0; i < panelDeSecuencia.transform.childCount; i++)
        {
            GameObject objeto = panelDeSecuencia.transform.GetChild(i).gameObject;

            listado.Add(objeto.name);
        }

        return listado;
    }

    private void ActivarCasillas(ArrayList listado, Color color)
    {
        posicionAnterior = ConseguirPosicionInicial();
        direccionRobot = robot.GetComponent<RobotMover>().ConseguirDireccion();
        BorrarCasillasActivadas();
        foreach (string item in listado)
        {
            //Debug.Log(item);

            switch (item)
            {
                case "FlechaArriba":
                    if (direccionRobot.Equals("arriba"))
                    {
                        PintarLaCasilla("arriba", posicionAnterior, color);
                    }
                    else if (direccionRobot.Equals("abajo"))
                    {
                        PintarLaCasilla("abajo", posicionAnterior, color);
                    }
                    else if (direccionRobot.Equals("izquierda"))
                    {
                        PintarLaCasilla("izquierda", posicionAnterior, color);
                    }
                    else if (direccionRobot.Equals("derecha"))
                    {
                        PintarLaCasilla("derecha", posicionAnterior, color);
                    }
                    break;
                case "FlechaAbajo":
                    if (direccionRobot.Equals("arriba"))
                    {
                        PintarLaCasilla("abajo", posicionAnterior, color);
                    }
                    else if (direccionRobot.Equals("abajo"))
                    {
                        PintarLaCasilla("arriba", posicionAnterior, color);
                    }
                    else if (direccionRobot.Equals("izquierda"))
                    {
                        PintarLaCasilla("derecha", posicionAnterior, color);
                    }
                    else if (direccionRobot.Equals("derecha"))
                    {
                        PintarLaCasilla("izquierda", posicionAnterior, color);
                    }
                    break;
                case "FlechaGirarIzquierda":
                    CambiarDireccionDelRobot("izquierda");
                    break;
                case "FlechaGirarDerecha":
                    CambiarDireccionDelRobot("derecha");
                    break;
                case "FlechaPausa":
                    break;
                default:
                    break;
            }

        }
    }

    private void CambiarDireccionDelRobot(string direccion)
    {
        if (direccion.Equals("izquierda"))
        {
            switch (direccionRobot)
            {
                case "arriba":
                    direccionRobot = "izquierda";
                    break;
                case "izquierda":
                    direccionRobot = "abajo";
                    break;
                case "abajo":
                    direccionRobot = "derecha";
                    break;
                case "derecha":
                    direccionRobot = "arriba";
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (direccionRobot)
            {
                case "arriba":
                    direccionRobot = "derecha";
                    break;
                case "izquierda":
                    direccionRobot = "arriba";
                    break;
                case "abajo":
                    direccionRobot = "izquierda";
                    break;
                case "derecha":
                    direccionRobot = "abajo";
                    break;
                default:
                    break;
            }
        }
    }

    private void CambiarEstadoDeCambio(bool estado)
    {
        hayCambioEnLaLista = estado;
    }

    private bool HayCambiosEnLaLista()
    {
        if (cantidadDeOrdenes != panelDeSecuencia.transform.childCount)
        {
            cantidadDeOrdenes = panelDeSecuencia.transform.childCount;
            return true;
        }
        else
        {
            return false;
        }
    }


    // Sirve para poner todas las casilla en transparente
    public void BorrarCasillasActivadas()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = colorCeldaTransparente;
        }
    }

    private bool PintarLaCasilla(string direccion, int posicionActual, Color color)
    {
        Debug.Log(color);
        switch (direccion)
        {
            case "arriba":
                // Comprobar si hay una casilla en la posición de arriba según la posición anterior
                if (posicionActual - cantidadDeColumnas >= 0)
                {
                    transform.GetChild(posicionActual - cantidadDeColumnas).GetComponent<Image>().color = color;
                    posicionAnterior = posicionActual - cantidadDeColumnas;
                }
                else
                {
                    hayError = true;
                }
                break;
            case "abajo":
                // Comprobar si hay una casilla en la posición de arriba según la posición anterior
                if (posicionActual + cantidadDeColumnas < gameObject.transform.childCount)
                {
                    transform.GetChild(posicionActual + cantidadDeColumnas).GetComponent<Image>().color = color;
                    posicionAnterior = posicionActual + cantidadDeColumnas;
                }
                else
                {
                    hayError = true;
                }
                break;
            case "izquierda":
                //Debug.Log(posicionActual);
                //Debug.Log(posicionActual%cantidadDeColumnas == 0);
                if (posicionActual % cantidadDeColumnas != 0)
                {
                    transform.GetChild(posicionActual - 1).GetComponent<Image>().color = color;
                    posicionAnterior = posicionActual - 1;
                }
                else
                {
                    hayError = true;
                }
                break;
            case "derecha":
                //Debug.Log((posicionActual+1f)%cantidadDeColumnas == 0);

                if ((posicionActual + 1f) % cantidadDeColumnas != 0)
                {
                    transform.GetChild(posicionActual + 1).GetComponent<Image>().color = color;
                    posicionAnterior = posicionActual + 1;
                }
                else
                {
                    hayError = true;
                }
                break;
            default:
                break;
        }
        return true;
    }

    private int ConseguirPosicionInicial()
    {
        if (!robot.GetComponent<RobotMover>().EstasEnCasita())
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).childCount > 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public void GestionarAyuda()
    {
        ayuda = !ayuda;

        if (ayuda)
        {
            DibujarCamino(true);
        }
        else
        {
            BorrarCasillasActivadas();
        }
    }

    public bool AyudaActivada()
    {
        return ayuda;
    }
}
