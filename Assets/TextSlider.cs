using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public Slider slider;
    //public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TextMeshPro>().text = "Hola";
        GetComponent<TextMeshProUGUI>().SetText("Hola");
    }

    // Update is called once per frame
    void Update()
    {
        
        //GetComponent<TextMesh>().text = "Hola";
    }

    public void CambiarTexto()
    {
        int valor = (int)slider.GetComponent<Slider>().value;
        GetComponent<TextMeshProUGUI>().SetText(valor.ToString());
    }
}
