using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Skeleton : MonoBehaviour
{
    public float velocidad; //Velocidad de movimiento
    public Vector3 posicionFin; //Posición a la que queremos que se desplace
    public Vector3 posicionInicio; //Posición actual
    private bool moviendoAFin; //Para saber si vamos en dirección a la posición final o ya estamos de vuelta
    
    //Variables de la barra de vida
    public float health = 100;
    public float maxHealth = 100;
    public FloatingHealthBar healthBar;

    bool followingPlayer = false;

    Rigidbody2D rb2d;

    public float moveSpeed;

    [SerializeField] float rayDistance = 1f;

    [SerializeField] LayerMask playerLayer;

    public GameObject obstacleRayObject;

    public bool dir = false;

    private bool chasing = false;

    public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicio = transform.position;  //Nos da la posición en la que estamos

        moviendoAFin = true;

        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);

        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitPlayerL = Physics2D.Raycast(obstacleRayObject.transform.position, Vector2.left, rayDistance, playerLayer);
        RaycastHit2D hitPlayerR = Physics2D.Raycast(obstacleRayObject.transform.position, Vector2.right, rayDistance, playerLayer);

        if (hitPlayerL.collider != null)
        {
            if (hitPlayerL.collider.gameObject.name == "Demon" && dir)
            {
                Debug.Log("chasing");
                chasing = true;
                ChasePlayer(hitPlayerL.collider.gameObject.transform.position);
            }

        }
        else if (hitPlayerR.collider != null)
        {
            if (hitPlayerR.collider.gameObject.name == "Demon" && dir == false)
            {
                Debug.Log("chasing");
                chasing = true;
                ChasePlayer(hitPlayerR.collider.gameObject.transform.position);
            }
        }
        else
        {
            Debug.Log("stop chasing");
            StopChasingPlayer();
            chasing = false;
        }

        if (followingPlayer == false)
        {
            if (!chasing)
            {
                MoverEnemigo();
            }
            
        }

    }

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
            Destroy(gameObject);
        }
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
}
