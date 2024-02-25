using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float posicionXAnterior;

    // Start is called before the first frame update
    void Start()
    {
        posicionXAnterior = transform.position.x;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sprite.name == "Skeleton") //Se comprueba la clase del que se le va a cambiar la direccion
        {
            GetComponent<Skeleton>().dir = !(posicionXAnterior < transform.position.x); //Es ! ... porque sino las direcciones se hacian contrarias con otras clases.
        }
        
        sprite.flipX = posicionXAnterior < transform.position.x;
        posicionXAnterior = transform.position.x;
    }
}
