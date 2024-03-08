using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
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

    public void OnBotonJugar()
    {
        SceneManager.LoadScene("Advertencia");
    }

    public void OnBotonCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void OnBotonSalir()
    {
        Application.Quit();
    }

    // Para hacer la vuelta al menú principal
    public void OnBotonMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnBotonContinuar()
    {
        SceneManager.LoadScene("Trono");
        gameManager.estadoInicial();
    }
}
