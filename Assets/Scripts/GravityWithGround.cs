using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWithGround : MonoBehaviour
{
    /*
     * 
     *  PARA QUE ESTO FUNCIONE EL SUELO DEBE DE ESTAR DENTRO DE LA CAPA 3 (GroundWalls).
     *  ADEMAS, EL PERSONAJE DEBE TENER UN Empty QUE INDIQUE A PARTIR DE DONDE SE COMPRUEBA SI ESTA
     *  TOCANDO EL SUELO
     * 
     * 
     */

    [SerializeField] public Transform groundCheck;
    [SerializeField] public Vector2 groundCheckSize = new Vector2 (0.5f, 0.5f);

    [SerializeField] public LayerMask groundMask;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundMask))
        {
            Debug.Log("floor");
        }
        else
        {
            Debug.Log("not floor");
            rb2d.AddForce(new Vector2(0f, -300f));
        }
    }
}
