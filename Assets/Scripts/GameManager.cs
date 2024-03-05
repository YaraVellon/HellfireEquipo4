using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameManager;

    // Para poder conservar la vida entre niveles y cambiarla (sube/baja) con power-ups
    public int vidasGlobal;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        vidasGlobal = 50;
        DontDestroyOnLoad(gameManager);
        cambiarEscena("Menu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cambiarEscena(string siguienteEscena)
    {
        SceneManager.LoadScene(siguienteEscena);
    }

    public int getVidas()
    {
        return vidasGlobal;
    }

    public void bajarVidas()
    {
        vidasGlobal--;
    }

    public void subirVidas()
    {
        vidasGlobal++;
    }
}
