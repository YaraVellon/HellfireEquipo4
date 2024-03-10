using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHudRanking : MonoBehaviour
{
    public Canvas canvas;
    private EscribirRanking hud;


    // Start is called before the first frame update
    void Start()
    {
        hud = canvas.GetComponent<EscribirRanking>();

        hud.setPrimeroTxt();
        hud.setSegundoTxt();
        hud.setTerceroTxt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
