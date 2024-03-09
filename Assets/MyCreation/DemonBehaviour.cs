using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehaviour : MonoBehaviour
{
    //Movimiento del jugador
    [Range(1, 10)] public float velocidad;
    Rigidbody2D rb2d;
    SpriteRenderer spRd;

    // Para el movimiento del jugador con joystick
    public Joystick joystick;
    private float movimientoV;
    private float movimientoH;

    //Salto de jugador
    //Para averiguar si esta saltando, asi se comprueba que no se pueda saltar varias veces en el aire
    bool isJumping = false;
    [Range(1, 500)] public float potenciaSalto;

    //Para la utilizacion del Animator del jugador
    private Animator animator;

    public bool dir;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRd = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //GRAVEDAD PARA QUE EL SALTO NO PAREZCA QUE ESTA EN LA LUNA
        rb2d.AddForce(new Vector2(0f,-1f));


        //RECOJO LOS VALORES QUE INDICAN EL MOVIMIENTO DEL PERSONAJE (1) - derecha , (-1) - izquierda, (0) - parado
        // inclusión del joystick; si los valores de movimiento superan una cierta sensibilidad, el movimiento horizontal
        // se pilla del joystick; si no, de los botones
        
        if (gameManager.getChaosMode() == false)
        {

            if (joystick.Horizontal > 0f | joystick.Horizontal < 0f)
            {
                movimientoH = joystick.Horizontal;
            }
            else
            {
                movimientoH = Input.GetAxisRaw("Horizontal");
            }

        } else
        {

            if (joystick.Horizontal > 0f | joystick.Horizontal < 0f)
            {
                movimientoH = -(joystick.Horizontal);
            }
            else
            {
                movimientoH = -(Input.GetAxisRaw("Horizontal"));
            }

        }





        rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);

        //Esto indica al Rigidbody2D la velocidad que queremos que tenga

        //EJE X : MovimientoH: para indicar la direccion del movimiento.
        //EJE Y : Obtengo la que tenia antes mediante rb2d.velocity.y


        if (movimientoH > 0)
        {
            dir = false;
            spRd.flipX = false;
        }
        else if (movimientoH < 0)
        {
            dir = true;
            spRd.flipX = true;
        }
        



        movimientoV = joystick.Vertical;
        if ((Input.GetButton("Jump") && !isJumping) || (movimientoV >= 0.5f && !isJumping))
        {
            rb2d.AddForce(Vector2.up * potenciaSalto);
            animator.SetBool("isJumping", true);
            isJumping = true;
        } else
        {
            movimientoV = 0f;
        }



        if (movimientoH != 0)
        {
            animator.SetBool("isWalking", true);
        }
        if (movimientoH == 0)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D otherobject)
    {
        if (otherobject.gameObject.CompareTag("Suelo"))
        {
            isJumping = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            animator.SetBool("isJumping", false);
        }
    }
}
