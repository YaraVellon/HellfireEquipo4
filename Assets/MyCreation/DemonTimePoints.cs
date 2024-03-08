using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonTimePoints : MonoBehaviour
{

    public Canvas canvas;
    private EscribirTiempoPuntuacion hud;
    private GameManager gameManager;


    private float momentoInicio; // Instante en que se empieza a jugar
    private int tiempoEmpleado; // El instante actual menos momentoInicio
    private int tiempoMAX; // El tiempo máximo disponible
    private int tiempoRestante;



    // Start is called before the first frame update
    void Start()
    {
        hud = canvas.GetComponent<EscribirTiempoPuntuacion>();
        hud.setPuntuacionesTxt();
        hud.setTiempoText();


        gameManager = FindObjectOfType<GameManager>();
        tiempoMAX = gameManager.getTiempoMax();
        momentoInicio = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // PARA EL CONTROL DEL TIEMPO
        tiempoEmpleado = (int)(Time.time - momentoInicio);
        tiempoRestante = tiempoMAX - tiempoEmpleado;

        gameManager.setTiempo(tiempoRestante);
        hud.setTiempoText();

    }
}
