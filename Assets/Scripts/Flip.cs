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

    private void Update()
    {
        GetComponent<Skeleton>().dir = !(posicionXAnterior < transform.position.x);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sprite.flipX = posicionXAnterior < transform.position.x;
        posicionXAnterior = transform.position.x;
    }
}
