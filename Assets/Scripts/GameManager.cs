using System;
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
    private float momentoInicio; // Instante en que se empieza a jugar
    private int tiempoEmpleado; // El instante actual menos momentoInicio

    // Para determinar el ending alcanzado
    private bool bossDificil;
    private bool entradaSalaBoss;
    private bool bossMuerto;

    // Para el daño de ataque (modificarlo y conservar modificaciones entre escenas)
    private int ataquePoderoso;
    private int ataqueDebil;
    private int attackDamage;

    // Para el modo caótico
    private bool chaosMode;
    private float momentoInicioChaosMode;
    private int tiempoTranscurridoChaosMode;

    // Para el fin de la partida
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        estadoInicial();

        DontDestroyOnLoad(gameManager);
        cambiarEscena("Menu");
    }

    public void estadoInicial()
    {
        health = 50;
        gameOver = false;

        puntuacion = 0;

        tiempoMAXIMO = 120;
        tiempoRestante = tiempoMAXIMO;
        momentoInicio = Time.time;


        bossDificil = false;
        entradaSalaBoss = false;
        bossMuerto = false;


        ataquePoderoso = 20;
        ataqueDebil = 5;
        attackDamage = ataquePoderoso;

        chaosMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (chaosMode)
        {
            tiempoTranscurridoChaosMode = (int)(Time.time - momentoInicioChaosMode);

            if (tiempoTranscurridoChaosMode == 30)
            {
                this.chaosMode = false;
                Debug.Log("MODO CAOS DESACTIVADO");
            }
        }


        if (tiempoRestante > 0 && (entradaSalaBoss==false))
        {
            tiempoEmpleado = (int)(Time.time - momentoInicio);
            tiempoRestante = tiempoMAXIMO - tiempoEmpleado;
            Debug.Log("Tiempo restante: " + tiempoRestante);
        }

        if (tiempoRestante == 0)
        {
            bossDificil = true;
        }

        if (gameOver)
        {
            SceneManager.LoadScene("GameOver");
            estadoInicial();
        }


        if (bossMuerto)
        {
            SceneManager.LoadScene("Menu");
            estadoInicial();
        }
    }

    public void setEntradaSalaBoss()
    {
        this.entradaSalaBoss = true;
    }

    public void setBossMuerto()
    {
        this.bossMuerto = true;
    }

    public void triggerChaosMode()
    {
        momentoInicioChaosMode = Time.time;
        this.chaosMode = true;
        Debug.Log("MODO CAOS ACTIVADO");
    }

    public bool getChaosMode()
    {
        return this.chaosMode;
    }

    public void bajarAtaque()
    {
        attackDamage = ataqueDebil;
        Debug.Log("EL VALOR DE ATAQUE ACTUAL ES " + attackDamage);
        Debug.Log("EL VALOR DE ATAQUE MÁXIMO ES " + ataquePoderoso);
    }

    public void subirAtaque()
    {
        attackDamage = ataquePoderoso;
        Debug.Log("EL VALOR DE ATAQUE ACTUAL ES " + attackDamage);
        Debug.Log("EL VALOR DE ATAQUE MÁXIMO ES " + ataquePoderoso);
    }

    public void sumarAtaque(int ataqueSumar)
    {
        ataquePoderoso += ataqueSumar;

        if (!(attackDamage == ataqueDebil))
        {
            attackDamage = ataquePoderoso;
        }

        Debug.Log("EL VALOR DE ATAQUE ACTUAL ES " + attackDamage);
        Debug.Log("EL VALOR DE ATAQUE MÁXIMO ES " + ataquePoderoso);
    }

    public void setGameOver()
    {
        this.gameOver = true;
    }

    public int getAttackDamage()
    {
        return this.attackDamage;
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

    public void subirVida(int vidaSube)
    {
        if ((health + vidaSube) <= 100)
        {
            health += vidaSube;
        } else
        {
            health = 100;
        }
        Debug.Log("VIDA ACTUAL: " + health);
    }

    public void bajarVida()
    {
        health -= 5;
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

    public void subirTiempo(int tiempoSube)
    {
        this.tiempoMAXIMO += tiempoSube;
    }

    public void bajarTiempo(int tiempoBaja)
    {
        this.tiempoMAXIMO -= tiempoBaja;
    }
}
