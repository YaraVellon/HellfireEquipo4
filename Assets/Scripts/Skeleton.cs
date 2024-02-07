using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(1, 10)] public float velocidad;
    Rigidbody2D rb2d;
    SpriteRenderer spRd;
    private Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRd = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");

        rb2d.velocity = new Vector2(movimientoHorizontal * velocidad, rb2d.velocity.y);

        if (movimientoHorizontal > 0)
        {
            spRd.flipX = false;
        }
        else if (movimientoHorizontal < 0)
        {
            spRd.flipX = true;
        }

        if (movimientoHorizontal != 0)
        {
            animator.SetBool("isSpawned", true);
        }
    }
}
