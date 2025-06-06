using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class controladorCanvas : MonoBehaviour
{
    //variables para cronometroUI
    [SerializeField] TextMeshProUGUI cronometroUI;
    float tiempo = 0f;
    bool activo = true;
    //variables para posicion
    [SerializeField] TextMeshProUGUI posicionUI;
    [SerializeField] List<string> posiciones;
    int LugarVariable = 1;
    //variables para num Vuelta
    [SerializeField] TextMeshProUGUI vueltaUI;
    int vuelta = 1;
    //variables para itemrandom
    [SerializeField] Image itemUI;
    [SerializeField] List<Sprite> items;
    private Coroutine ruletaItems;
    [Range(0.75f,1f)]
    [SerializeField] float tiempoTotal;
    [Range(0.045f, 0.085f)]
    [SerializeField] float intervalo;
    //WaitForSeconds intervaloRandom = new WaitForSeconds(0.01f);
    


    public void IniciarCorrutinaItems()
    {
        if (ruletaItems != null)
            StopCoroutine(ruletaItems);       
     
        ruletaItems = StartCoroutine(RandomizarItems());
           
    }

    IEnumerator RandomizarItems()
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < tiempoTotal )
        {
            int indexItem = Random.Range(0, items.Count);
            itemUI.sprite = items[indexItem];

            yield return new WaitForSeconds(intervalo);
            tiempoTranscurrido += intervalo;
        }

        int indexItemFinal = Random.Range(0,items.Count);
        itemUI.sprite = items[indexItemFinal];
    }
    // Start is called before the first frame update
    void Start()
    {
        //cronometroUI.text = "modificado";
        vueltaUI.text = $"{vuelta}";
        posicionUI.text = posiciones[posiciones.Count - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (activo)
        {
            tiempo += Time.deltaTime;
            ActualizarCronometro();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vuelta++;
            vueltaUI.text = $"{vuelta}";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LugarVariable++;
            int lugar = posiciones.Count - LugarVariable;
            posicionUI.text = posiciones[lugar];
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LugarVariable--;
            int lugar = posiciones.Count - LugarVariable;
            posicionUI.text = posiciones[lugar];
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IniciarCorrutinaItems();
            Debug.Log("funca");
        }
    }
    void ActualizarCronometro() 
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        int milesimas = Mathf.FloorToInt((tiempo * 100) % 100);

        cronometroUI.text = $"{minutos:00}:{segundos:00}:{milesimas:00}";
    }

//    void ItemRandom()
//    {
//        if (Input.GetKeyDown(KeyCode.P))
//        {
//            IniciarCorrutinaItems();
//            Debug.Log("funca");
//        }
//    }
}
