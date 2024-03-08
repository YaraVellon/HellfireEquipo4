using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caida : MonoBehaviour
{
    public string siguienteEscena;
    public int quitarVida;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el collider que colisiona es el asignado a un objeto
        // con la etiqueta del jugador
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(siguienteEscena);
            Destroy(gameObject);
            collision.gameObject.GetComponent<DemonHealth>().QuitarVida();
        }
    }
}
