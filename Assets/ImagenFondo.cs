using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagenFondo : MonoBehaviour
{
    public Canvas canvas;
    private float anchoContenedor, altoContenedor, anchoImagen, altoImagen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ArreglarGrilla();
    }


    void ArreglarGrilla()
    {
        // Conseguir el ancho y alto del contenedor
        anchoContenedor = canvas.GetComponent<RectTransform>().rect.width;
        altoContenedor = canvas.GetComponent<RectTransform>().rect.height;

        // Conseguir los nuevos valores para aplicar a la imagen de la alfombra
        anchoImagen = altoContenedor * GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.height;
        altoImagen = altoContenedor;

        // Cambiar el ancho y alto de la alfombra
        GetComponent<RectTransform>().sizeDelta = new Vector2(anchoImagen, altoImagen);


    }
}
