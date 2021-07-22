using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlay : MonoBehaviour
{
    public GameObject panelSecuencia;
    public GameObject robot;
    public GameObject botonCancelar;
    public GameObject botonPausa;
    public float waitTime = 3;
    //private bool ocupado = false;
    WaitForSecondsRealtime waitForSecondsRealtime;
    private ArrayList listaDeMovimientos = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        bool estaEnUnaCasilla;

        
        // Comprobar si el robot está en una casilla, de no estarlo, no hace nada.
        estaEnUnaCasilla = !robot.GetComponent<RobotMover>().EstasEnCasita();

        // Si está en la casilla, empieza a realizar la carga de las ordenes al robot.
        if (estaEnUnaCasilla && name.Equals("BotonIniciar"))
        {
            // Limpieza de la lista de movimientos a enviar al robot.
            listaDeMovimientos.Clear();

            // Carga de las ordenes en la lista a enviar al robot
            for (int i = 0; i < panelSecuencia.transform.childCount; i++)
            {
                listaDeMovimientos.Add(panelSecuencia.transform.GetChild(i).name);
            }
            // Se cambiar el tag al botón de cancelar para que cambie su comportamiento
            //botonCancelar.GetComponent<Button>().interactable = false;

            // Se cambia el nombre del botón pausa para poder pausar la secuencia
            botonCancelar.name = "BotonPausarSecuencia";
            
            // Se envia la lista de ordenes para que el robot las ejecute.
            robot.GetComponent<RobotMover>().CargarYEjecutar(listaDeMovimientos);


        }

    }


}
