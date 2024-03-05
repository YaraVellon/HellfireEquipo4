using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string siguienteEscena;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Método que hace que, al colisionar el collider del jugador y el del objeto
    // al que se le asigne el script, haya ejecución del método y se cambie la escena
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el collider que colisiona es el asignado a un objeto
        // con la etiqueta del jugador
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(siguienteEscena);
            Destroy(gameObject);
        }
    }
}
