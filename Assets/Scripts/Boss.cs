using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float velocidad; //Velocidad de movimiento
    public Vector3 posicionFin; //Posición a la que queremos que se desplace
    public Vector3 posicionInicio; //Posición actual
    private bool moviendoAFin; //Para saber si vamos en dirección a la posición final o ya estamos de vuelta

    //Variables de la barra de vida
    public float health = 100;
    public float maxHealth = 100;
    public FloatingHealthBar healthBar;
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb2d;
    public float moveSpeed;
    [SerializeField] float rayDistance = 1f;
    [SerializeField] LayerMask playerLayer;
    public GameObject obstacleRayObjectL;
    public GameObject obstacleRayObjectR;
    public bool dir = false;
    private bool chasing = false;
    public int damage;
    RaycastHit2D hitPlayerL;
    RaycastHit2D hitPlayerR;
    public float damageInterval = 3f;
    private float nextDamageTime;

    public Animator animator;

    private GameManager gameManager;
    private bool muerto;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        muerto = false;

        posicionInicio = transform.position;  //Nos da la posición en la que estamos

        moviendoAFin = true;

        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        nextDamageTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 boxSize = new Vector2(rayDistance, rayDistance);
        hitPlayerL = Physics2D.BoxCast(obstacleRayObjectL.transform.position, boxSize, 0f, Vector2.left, 0f, playerLayer);
        hitPlayerR = Physics2D.BoxCast(obstacleRayObjectR.transform.position, boxSize, 0f, Vector2.left, 0f, playerLayer);

        
        //Si la caja de vision izquierda no es null se comprueba que ha visto
        if (hitPlayerL.collider != null)
        {
            //Si lo que ha visto es al jugador se le persigue
            if (hitPlayerL.collider.gameObject.name == "Demon" && dir == true)
            {
                chasing = true;
                ChasePlayer(hitPlayerL.collider.gameObject.transform.position);
            }

            //Si la caja de vision derecha no es null se comprueba que ha visto
        }
        else if (hitPlayerR.collider != null)
        {
            //Si lo que ha visto es al jugador se le persigue
            if (hitPlayerR.collider.gameObject.name == "Demon" && dir == false)
            {
                chasing = true;
                ChasePlayer(hitPlayerR.collider.gameObject.transform.position);
            }
        }
        else
        {
            StopChasingPlayer();
            chasing = false;
        }
        if (!chasing)
        {

            MoverEnemigo();
        }
    }
    private void OnDrawGizmos()
    {
        Vector2 boxSize = new Vector2(rayDistance, rayDistance);
        Gizmos.DrawWireCube(obstacleRayObjectL.transform.position, boxSize);
        Gizmos.DrawWireCube(obstacleRayObjectR.transform.position, boxSize);
        Gizmos.color = Color.red;
    }
    private void MoverEnemigo()
    {
        Vector3 posiciondestino = (moviendoAFin) ? posicionFin : posicionInicio;
        transform.position = Vector3.MoveTowards(transform.position, posiciondestino, velocidad * Time.deltaTime);
        if (transform.position == posicionFin) moviendoAFin = false;
        if (transform.position == posicionInicio) moviendoAFin = true;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            healthBar.gameObject.SetActive(false);
            StartCoroutine(DestroyTimer());
            //Destroy(gameObject);
        }
    }
    IEnumerator DestroyTimer()
    {
        animator.Play("Death"); //Se reproduce la animacion de muerte
        yield return new WaitForSeconds(0.418f); //PARA SABER CUANTO TIEMPO TIENES QUE PONER AQUI, SIMPLEMENTE MIRAR LA DURACION DE LA ANIMACION o ir probando.
        if (!muerto)
        {

            muerto = true;

            if (gameManager.getBossDificil())
            {
                gameManager.subirPuntos(1000);
            }
            else
            {
                gameManager.subirPuntos(350);
            }


            Debug.Log("BOSS FINAL MUERTO.");
            gameManager.setBossMuerto();
        }
        Destroy(gameObject); //Se destruye al enemigo
    }
    void ChasePlayer(Vector3 playerpos)
    {

        if (transform.position.x < playerpos.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
        }
    }
    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InflictDamage(collision.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si el tiempo actual es mayor que el tiempo del próximo daño, infligir daño nuevamente
            if (Time.time >= nextDamageTime)
            {
                InflictDamage(collision.gameObject);
                nextDamageTime = Time.time + damageInterval; // Actualizar el tiempo del próximo daño
            }
        }
    }
    public void InflictDamage(GameObject player)
    {
        player.GetComponent<DemonHealth>().QuitarVidaCaida(damage);
    }
}
