using UnityEngine;
using UnityEngine.UI;

public class ListBluetoothScript : MonoBehaviour
{
    public Text text;
    private GameObject respawnPrefab;
    public Transform[] respawns;
    public Transform listado;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AgregarText()
    {
        Instantiate(text,listado);

        //respawns = GameObject.FindGameObjectsWithTag("Bluetooth");
        int cantHijos = listado.childCount;

        for (int i = 0; i < cantHijos; i++)
        {
            listado.GetChild(i).gameObject.SetActive(true);
        }
        /*
        foreach ( Transform transform in respawns) {
            Debug.Log("Verdadero");
        }*/
        
        
    }
}
