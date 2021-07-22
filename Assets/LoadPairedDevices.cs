using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPairedDevices : MonoBehaviour
{
    public GameObject bluetoothController;
    public GameObject dispositivos;
    //public GameObject toast;
    //public Text texto_toast;
    // Start is called before the first frame update
    void Start()
    {
        cargarListaDispositivos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cargarListaDispositivos(){
        List<string> lista = new List<string>();

        lista = bluetoothController.GetComponent<BluetoothHandlerSystems>().ObtenerDispositivos();

        Dropdown myDropDown = dispositivos.GetComponent<Dropdown>();
        myDropDown.options.Clear();
        foreach(string item in lista){
            string[] name_address = item.Split(';');
            if(name_address[0].Contains("KINDERBOT")){
                myDropDown.options.Add(new Dropdown.OptionData(name_address[0]));
            }
        }
        if(myDropDown.options.Count != 0){
            myDropDown.value = 0;
        }

      /*  Dropdown myDropDown = dispositivos.GetComponent<Dropdown>();
        myDropDown.options.Clear();
        myDropDown.options.Add(new Dropdown.OptionData("text1"));
        myDropDown.options.Add(new Dropdown.OptionData("text2"));*/

    }

    public void ConectarDispositivo(){
        List<string> lista = new List<string>();

        lista = bluetoothController.GetComponent<BluetoothHandlerSystems>().ObtenerDispositivos();

        Dropdown myDropDown = dispositivos.GetComponent<Dropdown>();
        if(myDropDown.value != -1){
            string elementoSeleccionado = myDropDown.options[myDropDown.value].text;

            foreach(string item in lista){
                string[] name_address = item.Split(';');
                if(name_address[0].Equals(elementoSeleccionado)){
                    bluetoothController.GetComponent<BluetoothHandlerSystems>().Conectar(name_address[1]);
                }
            }
        }

       /* List<string> lista = new List<string>();

        lista = bluetoothController.GetComponent<BluetoothHandlerSystems>().ObtenerDispositivos();
        texto_toast.text = "Hola!";
        string elementoSeleccionado = dispositivos.GetComponent<Dropdown>().itemText.text;
        texto_toast.text = elementoSeleccionado;
        foreach(string item in lista){
            string[] name_address = item.Split(';');
            if(name_address[0].Equals(elementoSeleccionado)){
                bluetoothController.GetComponent<BluetoothHandlerSystems>().Conectar(name_address[1]);
            }
        }*/


        //toast.GetComponent<ShowToast>().showToast(elementoSeleccionado,2);
        
    }
}
