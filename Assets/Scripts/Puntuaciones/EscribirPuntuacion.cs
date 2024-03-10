using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EscribirPuntuacion : MonoBehaviour
{
    public TextMeshProUGUI tiempoRestanteTxt;
    public TextMeshProUGUI puntuacionTxt;
    public TextMeshProUGUI puntuacionFinalTxt;
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

    public void setTiempoRestanteTxt()
    {
        int tiempoRestante = gameManager.getTiempo();
        tiempoRestanteTxt.text = "Tiempo restante: " + tiempoRestante + " segundos";
    }

    public void setPuntuacionTxt()
    {
        int puntuacion = gameManager.getPuntuacion();
        puntuacionTxt.text = "Puntuacion: " + puntuacion;
    }

    public void setPuntuacionFinalTxt()
    {
        int puntuacionFinal = gameManager.getPuntuacionFinal();
        puntuacionFinalTxt.text = "Puntuacion final: " + puntuacionFinal;
    }
}
