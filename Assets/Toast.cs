using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toast : MonoBehaviour
{
    private int tiempo = 0;
    private bool activado = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (tiempo > 0)
        {
            if (!activado)
            {
                
                gameObject.SetActive(true);
                activado = true;
            }
            tiempo--;
        }
        else
        {
            gameObject.SetActive(false);
            activado = false;
        }
    }

    public void MostrarMensaje(string mensaje)
    {
        
        gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(mensaje);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        tiempo = 120;
        Update();
    }
}
