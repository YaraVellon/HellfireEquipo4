using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EscribirTiempoPuntuacion : MonoBehaviour

{

    public TextMeshProUGUI tiempoTxt;
    public TextMeshProUGUI puntuacionesTxt;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPuntuacionesTxt()
    {
        int puntuacion = gameManager.getPuntuacion();
        puntuacionesTxt.text = "Puntuacion: " + puntuacion;
    }

    public void setTiempoText()
    {
        int tiempo = gameManager.getTiempo();
        // Quiero pasarlo a tiempo para que lo muestre como dos d�gitos de minutos y dos d�gitos de segundos
        // los dos cerillos es para darle formato de dos d�gitos

        if (tiempo >= 0)
        {
            int minutos = tiempo / 60;
            int segundos = tiempo % 60;
            tiempoTxt.text = "Tiempo: " + minutos.ToString("00") + " " + segundos.ToString("00");
        } else
        {
            tiempoTxt.text = "Tiempo: 00 00";
        }
    }
}
