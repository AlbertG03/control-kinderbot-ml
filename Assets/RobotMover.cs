using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RobotMover : MonoBehaviour
{
    public GraphicRaycaster graphicRaycaster;
    public GameObject objetoSeleccionado;
    public GameObject toastNoPuedoIrPorAhi;
    public GameObject botonDeModoSecuencia;
    public GameObject botonCancelar;
    public GameObject botonPlay;
    public GameObject botonPausa;
    public int cantidad = 1;
    public Text textoCantidad = null;
    public int ID;
    public GameObject conexionBluetooth;
    public GameObject casita;
    public GameObject grilla;
    public GameObject robotAmarillo;
    public GameObject robotAzul;
    public GameObject robotNaranja;
    public GameObject robotVerde;
    public GameObject robotRojo;
    public GameObject robotNegro;

    private List<RaycastResult> raycastResults;
    private Rigidbody2D robot;
    private float x, y = 0, width = 0;
    private int nuevaRotacion;
    private string direccion;
    private Collider m_Collider;
    private Vector3 m_Size;
    private bool enMovimiento = false;
    private bool modoSecuencia = false;
    private bool cancelarSecuencia = false;
    private bool play = false;
    private bool estaEnSecuencia = false;
    private string color = "rojo";

    // Habilitar secuencia por números
    /*
    private string texto_adelante = Char.ConvertFromUtf32(1);
    private string texto_atras = Char.ConvertFromUtf32(2);
    private string texto_derecha = Char.ConvertFromUtf32(3);
    private string texto_izquierda = Char.ConvertFromUtf32(4);
    private string texto_pausa = Char.ConvertFromUtf32(5);
    private string texto_iniciar = Char.ConvertFromUtf32(6);
    private string texto_parar_borrar = Char.ConvertFromUtf32(7);
    private string texto_mute = Char.ConvertFromUtf32(8);
    private string texto_modo = Char.ConvertFromUtf32(9);
    private string texto_bochinche = Char.ConvertFromUtf32(0);
    */

    // Habilitar secuencia por protocolo Lasffer
    private string texto_inicio_ms = "#inicio,ms;;;";
    private string texto_inicio_ml = "#inicio,ml;;;";
    private string texto_fin = "#fin;;;";
    private string texto_ms_adelante = "#s,a,100;;;";
    private string texto_ms_atras = "#s,r,100;;;";
    private string texto_ms_derecha = "#s,d,100;;;";
    private string texto_ms_izquierda = "#s,i,100;;;";
    private string texto_ml_adelante = "#ml,avanzar,1,100;;;";
    private string texto_ml_atras = "#ml,retroceder,1,100;;;";
    private string texto_ml_derecha = "#ml,girarDerecha,90,100;;;";
    private string texto_ml_izquierda = "#ml,girarIzquierda,90,100;;;";
    private string texto_ms_pausa = "#s,p;;;";
    private string texto_ms_iniciar = "#ms,play;;;";
    private string texto_ms_cancelar = "#s,c;;;";
    private string texto_mute = "#silenciar;;;";
    private string texto_modo_secuencia = "#modo_secuencia;;;";
    private string texto_modo_libre = "#modo_libre;;;";
    private string texto_bochinche = "#bochinche;;;";
    private string texto_emitir_sonido = "#sonido;;;";

    private void Start()
    {
        robot = GetComponent<Rigidbody2D>();
        nuevaRotacion = 0;
        direccion = "arriba";
        width = (int)GetComponent<RectTransform>().rect.width;
        CambiarColorA("amarillo");


    }

    public void Mover(string flechaApretada)
    {

        if (transform.parent.tag == "CeldaGrilla"
            && !enMovimiento
            && (!modoSecuencia || botonDeModoSecuencia.GetComponent<BtnComandos>().GetModoJoystick())
            && !transform.parent.name.Equals("Casita"))
        {
            //Debug.Log("Está en una celda");
            if (flechaApretada.Equals("adelante"))
            {
                if (conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EstaActivado() && !play)
                {

                    string textoAEnviar = texto_inicio_ml + texto_emitir_sonido + texto_ml_adelante + texto_fin;
                    Debug.Log(textoAEnviar);
                    conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);

                }
                if (direccion.Equals("arriba"))
                {
                    Arriba("avanzar");
                }
                else if (direccion.Equals("derecha"))
                {
                    Derecha("avanzar");
                }
                else if (direccion.Equals("izquierda"))
                {
                    Izquierda("avanzar");
                }
                else
                {
                    Abajo("avanzar");
                }


            }
            else if (flechaApretada.Equals("atras"))
            {
                if (conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EstaActivado() && !play)
                {

                    string textoAEnviar = texto_inicio_ml + texto_emitir_sonido + texto_ml_atras + texto_fin;
                    Debug.Log(textoAEnviar);
                    conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);

                }
                if (direccion.Equals("arriba"))
                {
                    //y += gameObject.transform.parent.GetComponent<RectTransform>().rect.height;
                    Abajo("atras");
                }
                else if (direccion.Equals("derecha"))
                {
                    Izquierda("atras");
                }
                else if (this.direccion.Equals("izquierda"))
                {
                    Derecha("atras");
                }
                else
                {
                    Arriba("atras");
                }


            }
            else if (flechaApretada.Equals("girarIzquierda"))
            {
                nuevaRotacion = 90;
                if (conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EstaActivado() && !play)
                {
                    string textoAEnviar = texto_inicio_ml + texto_emitir_sonido + texto_ml_izquierda + texto_fin;
                    Debug.Log(textoAEnviar);
                    conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                }
                if (direccion.Equals("arriba"))
                {
                    direccion = "izquierda";
                }
                else if (direccion.Equals("derecha"))
                {
                    direccion = "arriba";
                }
                else if (direccion.Equals("abajo"))
                {
                    direccion = "derecha";
                }
                else if (direccion.Equals("izquierda"))
                {
                    direccion = "abajo";
                }
            }
            else if (flechaApretada.Equals("girarDerecha"))
            {
                nuevaRotacion = -90;
                if (conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EstaActivado() && !play)
                {
                    string textoAEnviar = texto_inicio_ml + texto_emitir_sonido + texto_ml_derecha + texto_fin;
                    Debug.Log(textoAEnviar);
                    conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                }
                if (direccion.Equals("arriba"))
                {
                    direccion = "derecha";
                }
                else if (direccion.Equals("derecha"))
                {
                    direccion = "abajo";
                }
                else if (direccion.Equals("abajo"))
                {
                    direccion = "izquierda";
                }
                else if (direccion.Equals("izquierda"))
                {
                    direccion = "arriba";
                }
            }
            else if (flechaApretada.Equals("pausa"))
            {
                StartCoroutine(Pausa(2f));
            }
        }
    }

    public void PararYa()
    {
        cancelarSecuencia = true;
        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_ms_cancelar + texto_inicio_ms + texto_fin);
    }

    private void Update()
    {

        if (gameObject.transform.parent.tag == "CeldaGrilla")
        {
            float tamanioCelda = transform.parent.parent.GetComponent<GridLayoutGroup>().cellSize.x;

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, 2f);

            if (nuevaRotacion < 0)
            {
                robot.transform.Rotate(0, 0, -2);
                nuevaRotacion += 2;
            }
            else if (nuevaRotacion > 0)
            {
                robot.transform.Rotate(0, 0, 2);
                nuevaRotacion -= 2;
            }
            else
            {
                nuevaRotacion = 0;
            }

            if (y > 0)
            {
                robot.transform.Translate(new Vector3(0, 1, 0), robot.transform);
                y -= 1;
            }

            GetComponent<RectTransform>().sizeDelta = new Vector2(tamanioCelda, tamanioCelda);

        }
        else if (gameObject.transform.parent.tag == "Casita")
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, 10f);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(47, 47);

            if (!direccion.Equals("arriba"))
            {
                if (direccion.Equals("derecha"))
                {
                    nuevaRotacion = 90;
                }
                else if (direccion.Equals("abajo"))
                {
                    nuevaRotacion = 180;
                }
                else if (direccion.Equals("izquierda"))
                {
                    nuevaRotacion = -90;
                }
                direccion = "arriba";
            }

            if (nuevaRotacion < 0)
            {
                robot.transform.Rotate(0, 0, -1);
                nuevaRotacion += 1;
            }
            else if (nuevaRotacion > 0)
            {
                robot.transform.Rotate(0, 0, 1);
                nuevaRotacion -= 1;
            }
            else
            {
                nuevaRotacion = 0;
            }

            if (y > 0)
            {
                robot.transform.Translate(new Vector3(0, 1, 0), robot.transform);
                y -= 1;
            }


        }

        if (transform.localPosition != Vector3.zero || nuevaRotacion != 0)
        {
            enMovimiento = true;
        }
        else
        {
            enMovimiento = false;
        }


    }

    public void Arriba(string origen)
    {

        int nuevaPosicion;
        //Debug.Log(transform.parent.parent.GetComponent<Secuencia>().posicionCelda);
        nuevaPosicion = transform.parent.parent.GetComponent<Secuencia>().posicionCelda
            - transform.parent.parent.GetComponent<GridLayoutGroup>().constraintCount;
        //Debug.Log(nuevaPosicion);

        try
        {
            if (!cancelarSecuencia)
            {
                transform.SetParent(transform.parent.parent.GetChild(nuevaPosicion).transform);


            }
        }
        catch (System.Exception)
        {
            Debug.Log("No hay nada en esa casilla");
            if (play)
            {
                cancelarSecuencia = true;
            }
            StartCoroutine(MostrarToast("robot_movimientos_arriba"));
        }

    }

    public void Abajo(string origen)
    {
        int nuevaPosicion;
        nuevaPosicion = transform.parent.parent.GetComponent<Secuencia>().posicionCelda
            + transform.parent.parent.GetComponent<GridLayoutGroup>().constraintCount;
        try
        {
            if (!cancelarSecuencia)
            {
                transform.SetParent(transform.parent.parent.GetChild(nuevaPosicion).transform);
                /*
                if (conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EstaActivado() && !play)
                {
                    if (origen.Equals("avanzar"))
                    {
                        string textoAEnviar = texto_inicio_ml + texto_ml_adelante + texto_fin;
                        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                        //conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_adelante);
                    }
                    else
                    {
                        string textoAEnviar = texto_inicio_ml + texto_ml_atras + texto_fin;
                        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                        //conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_atras);
                    }
                }*/
            }
        }
        catch (System.Exception)
        {
            Debug.Log("No hay nada en esa casilla");
            if (play)
            {
                cancelarSecuencia = true;
            }
            StartCoroutine(MostrarToast("robot_movimientos_abajo"));
        }
    }

    public void Izquierda(string origen)
    {
        int nuevaPosicion;
        nuevaPosicion = transform.parent.parent.GetComponent<Secuencia>().posicionCelda;
        //Debug.Log((nuevaPosicion % transform.parent.parent.GetComponent<GridLayoutGroup>().constraintCount) != 0);
        if ((nuevaPosicion % transform.parent.parent.GetComponent<GridLayoutGroup>().constraintCount) != 0)
        {
            if (!cancelarSecuencia)
            {

                transform.SetParent(transform.parent.parent.GetChild(nuevaPosicion - 1).transform);
                /*
                if (conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EstaActivado() && !play)
                {
                    if (origen.Equals("avanzar"))
                    {
                        string textoAEnviar = texto_inicio_ml + texto_ml_adelante + texto_fin;
                        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                        //conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_adelante);
                    }
                    else
                    {
                        string textoAEnviar = texto_inicio_ml + texto_ml_atras + texto_fin;
                        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                        //conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_atras);
                    }
                }*/
            }
        }
        else
        {
            Debug.Log("No me puedo mover para ese lado");
            if (play)
            {
                cancelarSecuencia = true;
            }
            StartCoroutine(MostrarToast("robot_movimientos_izquierda"));
        }
    }

    public void Derecha(string origen)
    {
        int nuevaPosicion;
        nuevaPosicion = transform.parent.parent.GetComponent<Secuencia>().posicionCelda;
        //Debug.Log((nuevaPosicion % transform.parent.parent.GetComponent<GridLayoutGroup>().constraintCount) != 0);
        if (((nuevaPosicion + 1) % transform.parent.parent.GetComponent<GridLayoutGroup>().constraintCount) != 0)
        {
            if (!cancelarSecuencia)
            {
                transform.SetParent(transform.parent.parent.GetChild(nuevaPosicion + 1).transform);
                /*
                if (conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EstaActivado() && !play)
                {
                    if (origen.Equals("avanzar"))
                    {
                        string textoAEnviar = texto_inicio_ml + texto_ml_adelante + texto_fin;
                        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                        //conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_adelante);
                    }
                    else
                    {
                        string textoAEnviar = texto_inicio_ml + texto_ml_atras + texto_fin;
                        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(textoAEnviar);
                        //conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_atras);
                    }
                }*/
            }
        }
        else
        {
            Debug.Log("No me puedo mover para ese lado");
            if (play)
            {
                cancelarSecuencia = true;
            }
            StartCoroutine(MostrarToast("robot_movimientos_derecha"));
        }
    }

    public bool estaEnMovimiento()
    {
        return enMovimiento;
    }

    public bool EsPosibleAgarrarlo()
    {
        return !(enMovimiento || estaEnSecuencia);
    }

    public void CargarYEjecutar(ArrayList lista)
    {
        cancelarSecuencia = false;
        StartCoroutine(Ejecutar(lista));

        //yield return new WaitForSeconds(3);
    }

    IEnumerator Ejecutar(ArrayList lista)
    {
        // La variable que acumulará las ordenes a enviar
        string cadenaDeValores = "";
        estaEnSecuencia = true;
        modoSecuencia = false;
        play = true;
        botonPausa.GetComponent<Button>().interactable = false;
        //botonCancelar.GetComponent<Button>().interactable = false;
        botonPlay.GetComponent<Button>().interactable = false;
        //ArrayList listaAEnviar = new ArrayList();
        // Generar el string a mandar al robot
        foreach (string item in lista)
        {
            String texto = texto_inicio_ms;
            switch (item)
            {
                case "FlechaArriba":
                    texto = texto_ms_adelante;
                    break;
                case "FlechaAbajo":
                    texto = texto_ms_atras;
                    break;
                case "FlechaGirarIzquierda":
                    texto = texto_ms_izquierda;
                    break;
                case "FlechaGirarDerecha":
                    texto = texto_ms_derecha;
                    break;
                case "FlechaPausa":
                    texto = texto_ms_pausa;
                    break;
                default:
                    break;
            }
            //listaAEnviar.Add(texto);
            cadenaDeValores += texto;
        }

        cadenaDeValores += texto_ms_iniciar + texto_fin + texto_emitir_sonido + texto_fin;
        //listaAEnviar.Add("#ms,play;;;#fin;;;");
        Debug.Log(cadenaDeValores);
        conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(cadenaDeValores);

        /*foreach (string item in listaAEnviar)
        {
            conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(item);
        }*/


        for (int i = 0; i < lista.Count; i++)
        {
            if (!estaEnMovimiento() && !cancelarSecuencia)
            {
                switch (lista[i].ToString())
                {
                    case "FlechaArriba":
                        Debug.Log("Flecha arriba");
                        Mover("adelante");
                        break;
                    case "FlechaAbajo":
                        Debug.Log("Flecha abajo");
                        Mover("atras");
                        break;
                    case "FlechaGirarIzquierda":
                        Debug.Log("Flecha girar izquierda");
                        Mover("girarIzquierda");
                        break;
                    case "FlechaGirarDerecha":
                        Debug.Log("Flecha girar derecha");
                        Mover("girarDerecha");
                        break;
                    case "FlechaPausa":
                        Debug.Log("Pausa 1 segundo");
                        Mover("pausa");
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(1);
            }
            else if (!cancelarSecuencia)
            {
                yield return new WaitForSeconds(1);
                i--;
            }


        }
        modoSecuencia = true;
        if (cancelarSecuencia)
        {
            botonCancelar.name = "BotonCancelar";
        }
        cancelarSecuencia = false;
        estaEnSecuencia = false;
        play = false;
        botonCancelar.name = "BotonCancelar";
        botonCancelar.GetComponent<Button>().interactable = true;
        botonPausa.GetComponent<Button>().interactable = true;
        botonPlay.GetComponent<Button>().interactable = true;
        //botonPausa.name = "BotonPausa";
    }

    IEnumerator Pausa(float segundos)
    {
        yield return new WaitForSeconds(segundos);
    }

    /*
    IEnumerator MostrarToast()
    {
        toastNoPuedoIrPorAhi.SetActive(true);
        toastNoPuedoIrPorAhi.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);
        toastNoPuedoIrPorAhi.SetActive(false);

    }*/

    public void CambiarModoFrecuencia(bool tipo)
    {
        modoSecuencia = tipo;
        //conexionBluetooth.GetComponent<BluetoothHandlerSystems>().EnviarString(texto_modo);
        cancelarSecuencia = false;
    }

    public bool EstasEnCasita()
    {
        return transform.parent.name == "Casita";
    }

    public void IrACasa()
    {
        transform.SetParent(casita.transform);
    }

    public void CambiarColorA(string color)
    {
        this.color = color;

        switch (color)
        {
            case "rojo":
                GetComponent<RawImage>().texture = robotRojo.GetComponent<RawImage>().texture;
                break;
            case "amarillo":
                GetComponent<RawImage>().texture = robotAmarillo.GetComponent<RawImage>().texture;
                break;
            case "azul":
                GetComponent<RawImage>().texture = robotAzul.GetComponent<RawImage>().texture;
                break;
            case "verde":
                GetComponent<RawImage>().texture = robotVerde.GetComponent<RawImage>().texture;
                break;
            case "naranja":
                GetComponent<RawImage>().texture = robotNaranja.GetComponent<RawImage>().texture;
                break;
            case "negro":
                GetComponent<RawImage>().texture = robotNegro.GetComponent<RawImage>().texture;
                break;
            default:
                break;
        }
    }

    IEnumerator MostrarToast(string animacion)
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play(animacion);
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().enabled = false;

    }

    public bool GetModo()
    {
        return modoSecuencia;
    }

    public string ConseguirDireccion()
    {
        return direccion;
    }
}
