using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealth : MonoBehaviour
{
    //Variables de la barra de vida
    [SerializeField] public float health = 100; //Vida al aparecer en el mapa
    [SerializeField] public float maxHealth = 100; //Vida maxima
    [SerializeField] DemonHealthBar healthBar; //Barra de vida flotante (slider) del enemigo

    // Start is called before the first frame update
    void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.UpdateHealthBar(health, maxHealth);
    }
}
