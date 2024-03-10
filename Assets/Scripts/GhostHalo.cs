using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHalo : MonoBehaviour
{
    public float velocidad;
    public Vector3 posicionFin;
    public Vector3 posicionInicio;
    private bool moviendoAFin;

    [SerializeField] public float health = 100;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] FloatingHealthBar healthBar;

    [SerializeField] Rigidbody2D rb2d;
    public float moveSpeed;
    [SerializeField] float rayDistance = 1f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject obstacleRayObjectL;
    [SerializeField] GameObject obstacleRayObjectR;
    public bool dir = false;
    private bool chasing = false;
    [SerializeField] RaycastHit2D hitPlayerL;
    [SerializeField] RaycastHit2D hitPlayerR;

    [SerializeField] Animator animator;

    private GameManager gameManager;
    private bool muerto;
    public float damageInterval = 3f;
    private float nextDamageTime;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        muerto = false;

        posicionInicio = transform.position;  //Nos da la posición en la que estamos

        moviendoAFin = true;

        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
        nextDamageTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Este vector indica lo grande que es la caja de vision del enemigo hacia cada direccion
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

        //Segun el valor de la variable CHASING - esta se activa si el enemigo esta persiguiendo al jugador el enemigo vuelve a hacer su camino o para
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

    //Método que declara al enemigo hacia donde tiene que ir
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
            if (!muerto)
            {
                muerto = true;
                gameManager.subirPuntos(150);
                Debug.Log("ENEMIGO MUERTO.");
            }
            //Esconde la barra de vida si el enemigo a muerto
            healthBar.gameObject.SetActive(false);
            StartCoroutine(DestroyTimer()); //Comienza una corutina para ejecutar la animacion de muerte antes de destruir al enemigo

            //Destroy(gameObject);
        }
    }

    IEnumerator DestroyTimer()
    {
        animator.Play("Death"); //Se reproduce la animacion de muerte
        yield return new WaitForSeconds(0.418f); //PARA SABER CUANTO TIEMPO TIENES QUE PONER AQUI, SIMPLEMENTE MIRAR LA DURACION DE LA ANIMACION o ir probando.
        Destroy(gameObject); //Se destruye al enemigo
    }

    //Método para perseguir al jugador. Funciona de manera que recoge la posicion del jugador y se le manda a esa.
    void ChasePlayer(Vector3 playerpos)
    {

        if (transform.position.x < playerpos.x)
        {
            if (transform.position.y < playerpos.y)
            {
                rb2d.velocity = new Vector2(moveSpeed, moveSpeed);
            } else if (transform.position.y > playerpos.y)
            {
                rb2d.velocity = new Vector2(moveSpeed, -moveSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(moveSpeed, 0);
            }
            
        }
        else
        {
            if (transform.position.y < playerpos.y)
            {
                rb2d.velocity = new Vector2(-moveSpeed, moveSpeed);
            }
            else if (transform.position.y > playerpos.y)
            {
                rb2d.velocity = new Vector2(-moveSpeed, -moveSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(-moveSpeed, 0);
            }
        }
    }

    //Se para la persecucion del enemigo
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
        player.GetComponent<DemonHealth>().QuitarVida();
    }
}
