using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        posicionInicio = transform.position;  //Nos da la posición en la que estamos

        moviendoAFin = true;

        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(followingPlayer == false)
        {
            MoverEnemigo();
        }
    }

    public void stopMovement()
    {
        followingPlayer = true;
    }

    public void keepMoving()
    {
        followingPlayer = false;
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
            Destroy(gameObject);
        }
    }
}
