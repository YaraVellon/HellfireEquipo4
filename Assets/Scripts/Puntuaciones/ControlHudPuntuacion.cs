using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHudPuntuacion : MonoBehaviour
{
    public Canvas canvas;
    private EscribirPuntuacion hud;


    // Start is called before the first frame update
    void Start()
    {
        hud = canvas.GetComponent<EscribirPuntuacion>();

        hud.setTiempoRestanteTxt();
        hud.setPuntuacionTxt();
        hud.setPuntuacionFinalTxt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
