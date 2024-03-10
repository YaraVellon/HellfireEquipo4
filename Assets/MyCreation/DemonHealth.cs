using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealth : MonoBehaviour
{
    //Variables de la barra de vida
    [SerializeField] public float health; //Vida al aparecer en el mapa
    [SerializeField] public float maxHealth = 100; //Vida maxima
    [SerializeField] DemonHealthBar healthBar; //Barra de vida flotante (slider) del enemigo

    public Animator animator;

    // GameManager para que la vida actual se quede almacenada ahí
    // Se guarda a través de las escenas; para mostrarla en la health bar, se recupera el valor ahí almacenado
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        health = gameManager.getHealth();

        animator = GetComponent<Animator>();

        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        health = gameManager.getHealth();
        healthBar.UpdateHealthBar(health, maxHealth);


        if (gameManager.getHealth() <= 0)
        {
            healthBar.gameObject.SetActive(false);
            StartCoroutine(DestroyTimer());
        }
    }

    IEnumerator DestroyTimer()
    {
        animator.Play("Death"); //Se reproduce la animacion de muerte
        yield return new WaitForSeconds(0.5f); //PARA SABER CUANTO TIEMPO TIENES QUE PONER AQUI, SIMPLEMENTE MIRAR LA DURACION DE LA ANIMACION o ir probando.
        Destroy(gameObject); //Se destruye el personaje
        gameManager.setGameOver(); // Se settea el game over como true y salta la pantalla de has muerto
    }

    public void QuitarVida()
    {
        gameManager.bajarVida();
        gameManager.bajarPuntos(5);
        gameManager.bajarTiempo(1);

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
