using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alfombras : MonoBehaviour
{
    public int id = 0;
    public bool activado;
    // Start is called before the first frame update
    void Start()
    {
        activado = gameObject.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Desactivar()
    {
        activado = false;
        gameObject.SetActive(false);
        
    }
    public void Activar()
    {
        activado = true;
        gameObject.SetActive(true);
    }
}
