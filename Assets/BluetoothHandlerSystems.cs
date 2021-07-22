using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class BluetoothHandlerSystems : MonoBehaviour
{
    private AndroidJavaObject bluetoothImp;
    private string deviceId = "00:19:10:08:4C:AD";
    private static string instructions = "#s,a,100;;;#fin;;;";
    public GameObject dispositivos;
    public GameObject botonDesconectar;
    public GameObject botonConectar;
    public GameObject robot;

    GameObject dialog = null;

    public void bluetooth_Imp(){
        bluetoothImp = new AndroidJavaObject("com.demgo.bluetoothml.TestBluetooth");
    }

    // Start is called before the first frame update
    void Start()
    {
        bluetooth_Imp();

        #if PLATFORM_ANDROID
        /*if(!Permission.HasUserAuthorizedPermission(Permission.Bluetooth)){
            Permission.RequestUserPermission(Permission.Bluetooth);
            dialog = new GameObject();
        }*/
        #endif        
    }

    void OnGUI(){
        #if PLATFORM_ANDROID
        if(!EstaActivado()){
            ActivarBluetooth();
        }
       /* if(!Permission.HasUserAuthorizedPermission(Permission.Bluetooth)){
            dialog.AddComponent<PermissionsRationaleDialog>();
            return;
        }
        else if(dialog != null){
            Destroy(dialog);
        }*/
        #endif
    }

    public void EnviarString(string comandos){
        if(bluetoothImp.Call<bool>("isActive")){
            if(EstaConectado()){
                //this.bluetoothImp.Call("connect", deviceId);
                bluetoothImp.Call("write",comandos);
                //this.bluetoothImp.Call("disconnect");
            }else{
                //MostrarMensajeToast("Debe conectar un kinderbot primero.");
            }
        }else{
            ActivarBluetooth();
        }
    }
    
    public bool EstaActivado(){
        return bluetoothImp.Call<bool>("isActive");
    }

    public List<string> ObtenerDispositivos(){

        AndroidJavaObject array = new AndroidJavaObject("java.util.ArrayList");
        array = bluetoothImp.Call<AndroidJavaObject>("searchPaired");
        int cant = array.Call<int>("size");

        List<string> paired = new List<string>();
        for(int i = 0; i < cant; i ++){
            paired.Add(array.Call<string>("get", i));
        }
        
        return paired;

    }
    //LoadPairedDevices
    public void CargarListaDispositivos(){
        List<string> lista = new List<string>();

        lista = ObtenerDispositivos();

        Dropdown myDropDown = dispositivos.GetComponent<Dropdown>();
        myDropDown.options.Clear();
        
        foreach(string item in lista){
            string[] name_address = item.Split(';');
            if(name_address[0].Contains("KINDERBOT")){
                myDropDown.options.Add(new Dropdown.OptionData(name_address[0]));
            }
        }

        if(myDropDown.options.Count != 0){
            myDropDown.value = 1;
            myDropDown.value = 0;
           // myDropDown.itemText.text = myDropDown.options[myDropDown.value].text;
        }

    }
    //LoadPairedDevices
    public void ConectarDispositivo(){
        List<string> lista = new List<string>();

        lista = ObtenerDispositivos();

        Dropdown myDropDown = dispositivos.GetComponent<Dropdown>();
        if(myDropDown.value != -1){
            string elementoSeleccionado = myDropDown.options[myDropDown.value].text;

            foreach(string item in lista){
                string[] name_address = item.Split(';');
                if(name_address[0].Equals(elementoSeleccionado)){
                    if(Conectar(name_address[1])){
                        string[] nombre = elementoSeleccionado.Split(' ');
                        
                        if(robot != null){
                            robot.GetComponent<RobotMover>().CambiarColorA(nombre[1].ToLower());
                        }

                        botonConectar.GetComponent<Button>().interactable = false;
                        botonDesconectar.GetComponent<Button>().interactable = true;
                    }
                }
            }
        }

    }

    public bool Conectar(string _deviceId){
        if(bluetoothImp.Call<bool>("isActive")){
            ActualizaIdDispositivo(_deviceId);
            bluetoothImp.Call("connect", deviceId);
            if(EstaConectado()){
                bluetoothImp.Call("write","#inicio,ml;;;#sonido;;;#fin;;;");
                return true;
            }else{
                MostrarMensajeToast("No se pudo conectar el kinderbot seleccionado.");
                return false;
            }
        }else{
            ActivarBluetooth();
        }
        return false;
    }
    public void Desconectar(){
        if(EstaActivado()){
            botonConectar.GetComponent<Button>().interactable = true;
            botonDesconectar.GetComponent<Button>().interactable = false;
            if(EstaConectado()){
                if(robot != null){
                    robot.GetComponent<RobotMover>().CambiarColorA("amarillo");
                }
                bluetoothImp.Call("write","#inicio,ml;;;#sonido;;;#fin;;;");
                bluetoothImp.Call("disconnect");
            }
        }
    }

    public void ActualizaIdDispositivo(string id){
        deviceId = id;
    }

    public void ActivarBluetooth(){
        AndroidJavaObject unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass adapterActivity = new AndroidJavaClass("com.demgo.bluetoothml.TestBluetooth");
        adapterActivity.CallStatic("newActivity", activity);
    }
    
    public bool EstaConectado(){
        AndroidJavaObject device = bluetoothImp.Call<AndroidJavaObject>("getDevice", deviceId);
        bool conectado = bluetoothImp.CallStatic<bool>("isConnected",device);
        return conectado;
    }
    public void MostrarMensajeToast(string mensaje){
        
        AndroidJavaObject unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass adapterActivity = new AndroidJavaClass("com.demgo.bluetoothml.TestBluetooth");
        adapterActivity.CallStatic("showToast", activity, 0, mensaje);
    
    }

   /* IEnumerator EnviaComandos(string input){

        string [] comandos = input.split(";;;");
                for(int i = 0; i < comandos.length; i ++){
                    String temp = comandos[i]+";;;";
                    mmOutStream.write(temp.getBytes());
                    SystemClock.sleep(50);
                }

        yield return new WaitForSeconds(1);
    }*/
    
}
