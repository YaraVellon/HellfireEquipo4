using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameManager;

    // Para poder conservar la vida entre escenas y cambiarla (sube/baja) con power-ups
    private float health;

    // Para también conservar entre escenas la puntuación y el tiempo restante en segundos
    private int puntuacion;
    private int tiempoRestante;
    private int tiempoMAXIMO;

    // Para determinar el ending alcanzado
    private bool bossDificil;

    // Para el daño de ataque (modificarlo y conservar modificaciones entre escenas)
    private int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        health = 50;
        puntuacion = 0;
        tiempoMAXIMO = 120;
        tiempoRestante = tiempoMAXIMO;
        bossDificil = false;
        attackDamage = 20;


        DontDestroyOnLoad(gameManager);
        cambiarEscena("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempoRestante == 0)
        {
            bossDificil = true;
        }
    }

    public int getAttackDamage()
    {
        return this.attackDamage;
    }

    public void subirAtaque(int ataqueSubir)
    {
        this.attackDamage += ataqueSubir;
    }

    public bool getBossDificil()
    {
        return this.bossDificil;
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
        health += 10;
    }

    public void bajarVida()
    {
        health -= 10;
    }

    public void bajarVida(int vidaPierde)
    {
        health -= vidaPierde;
    }


    public int getPuntuacion()
    {
        return this.puntuacion;
    }

    public void subirPuntos(int puntosSube)
    {
        this.puntuacion += puntosSube;
    }

    public void bajarPuntos (int puntosBaja)
    {
        this.puntuacion -= puntosBaja;
    }


    public int getTiempoMax()
    {
        return this.tiempoMAXIMO;
    }

    public int getTiempo()
    {
        return this.tiempoRestante;
    }

    public void setTiempo(int restante)
    {
        this.tiempoRestante = restante;
    }

    public void subirTiempo(int tiempoSube)
    {
        this.tiempoRestante += tiempoSube;
    }

    public void bajarTiempo(int tiempoBaja)
    {
        this.tiempoRestante -= tiempoBaja;
    }
}
