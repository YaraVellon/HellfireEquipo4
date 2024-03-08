using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonTimePoints : MonoBehaviour
{

    public Canvas canvas;
    private EscribirTiempoPuntuacion hud;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        hud = canvas.GetComponent<EscribirTiempoPuntuacion>();
        hud.setPuntuacionesTxt();
        hud.setTiempoText();


        gameManager = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        hud.setTiempoText();
    }
}
