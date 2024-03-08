using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameManager;

    // Para poder conservar la vida entre niveles y cambiarla (sube/baja) con power-ups
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        health = 100;
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

    public float getHealth()
    {
        return health;
    }

    public void subirVida()
    {
        health++;
    }

    public void bajarVida()
    {
        health -= 10;
    }

    public void bajarVida(int vidaPierde)
    {
        health -= vidaPierde;
    }
}
