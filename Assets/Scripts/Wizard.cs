using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public float velocidad; //Velocidad de movimiento
    public Vector3 posicionFin; //Posición a la que queremos que se desplace
    public Vector3 posicionInicio; //Posición actual
    private bool moviendoAFin; //Para saber si vamos en dirección a la posición final o ya estamos de vuelta

    //Variables de la barra de vida
    [SerializeField] public float health = 100; //Vida al aparecer en el mapa
    [SerializeField] public float maxHealth = 100; //Vida maxima
    public FloatingHealthBar healthBar; //Barra de vida flotante (slider) del enemigo

    //Variables para la persecucion del personaje 
    Rigidbody2D rb2d; //RigidBody del enemigo. Con esta variable se le modifica la velocidad al enemigo cuando vea al personaje
    public float moveSpeed; //Velocidad que tomara el enemigo cuando vea al personaje
    [SerializeField] float rayDistance = 1f; //Distancia de vision (si no esta en 6 hay que modificar las posiciones de las demas cosas)
    [SerializeField] LayerMask playerLayer; //Capa donde se encuentra el jugador
    public GameObject obstacleRayObjectL; //Punto desde donde comienza la vision de la izquierda
    public GameObject obstacleRayObjectR; //Punto desde donde comienza la vision de la derecha
    public bool dir = false; //Direccion hacia la que camina el enemigo; if true -- izquierda, if false -- derecha. ESTA SE MODIFICA EN EL SCRIPT DE FLIP
    private bool chasing = false; //Variable que indica si el enemigo esta con el comportamiento de persecucion
    RaycastHit2D hitPlayerL; //Listener de colisiones del area izquierda
    RaycastHit2D hitPlayerR; //Listener de colisiones del area derecha

    //Variables para las particulas del movimiento
    //(SE TIENE QUE PONER QUE SOLO SE MUESTREN SI EL ENEMIGO ESTA MOVIENDOSE PERO COMO NO TIENE UNA ANIMACION DE IDLE PUES NO SE COMO HACERLO)
    public ParticleSystem dust;

    //Variables para la muerte del enemigo
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicio = transform.position;  //Nos da la posición en la que estamos

        moviendoAFin = true;

        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
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
        dust.Play();
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
            rb2d.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    //Se para la persecucion del enemigo
    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
}
