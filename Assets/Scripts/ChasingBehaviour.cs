using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBehaviour : MonoBehaviour
{
    public Transform castPoint;
    public Transform player;
    Rigidbody2D rb2d;
    public float moveSpeed;
    public float agroRange;

    bool isFacingLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            if(rb2d.name == "Skeleton"){
                rb2d.GetComponent<Skeleton>().stopMovement();
            }else if (rb2d.name == "BurningGhoul")
            {
                //rb2d.GetComponent<BurningGhoul>().stopMovement();
            }
            
            ChasePlayer();
        }
        else
        {
            if (rb2d.name == "Skeleton")
            {
                rb2d.GetComponent<Skeleton>().keepMoving();
            }
            else if (rb2d.name == "BurningGhoul")
            {
                //rb2d.GetComponent<BurningGhoul>().stopMovement();
            }
            
            StopChasingPlayer();
        }
    }

    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            isFacingLeft = true;
        }
        else
        {
            rb2d.velocity = new Vector2 (-moveSpeed, 0);
            isFacingLeft = false;
        }
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2 (0, 0);
    }
}
