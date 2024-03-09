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
            // Cambios a aplicar cuando la escena a cargar sea la de vuelta
            if (siguienteEscena.Equals("SceneVuelta"))
            {
                gameManager.bajarAtaque();
            }


            // Cuando la escena a cargar sea TronoFinal:
            if (siguienteEscena.Equals("TronoFinal"))
            {
                // Se verifica si se ha activado o no la pelea final en modo difícil
                bool hard = gameManager.getBossDificil();

                if (hard)
                {
                    // Si es la versión difícil, se carga TronoFinalHARD y se baja el daño
                    siguienteEscena = "TronoFinalHARD";
                    gameManager.bajarAtaque();

                } else
                {
                    // Si es la versión normal, se carga TronoFinal y se baja el daño
                    // la variable siguienteEscena ya es TronoFinal
                    gameManager.subirAtaque();
                }
            }
            

            SceneManager.LoadScene(siguienteEscena);
            Destroy(gameObject);
        }
    }
}
