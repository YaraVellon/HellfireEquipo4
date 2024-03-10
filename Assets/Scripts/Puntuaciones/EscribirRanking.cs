using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EscribirRanking : MonoBehaviour
{

    public TextMeshProUGUI primeroTxt;
    public TextMeshProUGUI segundoTxt;
    public TextMeshProUGUI terceroTxt;
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

    public void setPrimeroTxt()
    {
        int primero = gameManager.getPrimero();
        primeroTxt.text = "PRIMER PUESTO \t-----  " + primero + " puntos";
    }

    public void setSegundoTxt()
    {
        int segundo = gameManager.getSegundo();
        segundoTxt.text = "SEGUNDO PUESTO\t-----  " + segundo + " puntos";
    }

    public void setTerceroTxt()
    {
        int tercero = gameManager.getTercero();
        terceroTxt.text = "TERCER PUESTO\t-----  " + tercero + " puntos";
    }
}
