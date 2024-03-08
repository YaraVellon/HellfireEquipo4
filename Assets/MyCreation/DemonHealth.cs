using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealth : MonoBehaviour
{
    //Variables de la barra de vida
    [SerializeField] public float health; //Vida al aparecer en el mapa
    [SerializeField] public float maxHealth = 100; //Vida maxima
    [SerializeField] DemonHealthBar healthBar; //Barra de vida flotante (slider) del enemigo

    // GameManager para que la vida actual se quede almacenada ahí
    // Se guarda a través de las escenas; para mostrarla en la health bar, se recupera el valor ahí almacenado
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        health = gameManager.getHealth();

        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.UpdateHealthBar(health, maxHealth);
    }
    public void QuitarVida()
    {
        gameManager.bajarVida();
        this.health = gameManager.getHealth();
        Debug.Log(this.health);
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    public void QuitarVidaCaida(int vida)
    {
        gameManager.bajarVida(vida);
        this.health = gameManager.getHealth();
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}
