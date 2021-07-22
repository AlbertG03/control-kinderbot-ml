using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnConnect : MonoBehaviour
{

    public GameObject datosConeccion;
    public Text text_show;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void conectar(){
        int index = datosConeccion.GetComponent<Dropdown>().value;
        string texto = datosConeccion.GetComponent<Dropdown>().options[index].text;
       
    }
}
