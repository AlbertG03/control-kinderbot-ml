using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    
    public Animator animacionTransicion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeScene(string nombreEscena)
    {
        if (nombreEscena.Equals("cerrarAplicacion"))
        {
            Application.Quit();
        }
        else
        {

            StartCoroutine(CargarEscena(nombreEscena));

        }
    }

    IEnumerator CargarEscena(string nombreEscena)
    {
        //panelDeTransicion.GetComponent<PanelDeTransicion>().setComenzarFin(true);
        animacionTransicion.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nombreEscena);
    }

}
