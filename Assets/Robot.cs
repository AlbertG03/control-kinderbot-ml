using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    public int cantidad = 1;
    public Text textoCantidad;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        textoCantidad.text = cantidad.ToString();
    }
}
