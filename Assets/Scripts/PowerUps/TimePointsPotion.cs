using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePointsPotion : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.subirPuntos(30);
            gameManager.subirTiempo(5);
            Destroy(gameObject);
        }
    }
}
