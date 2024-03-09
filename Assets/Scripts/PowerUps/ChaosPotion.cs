using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosPotion : MonoBehaviour
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
            gameManager.triggerChaosMode();
            Destroy(gameObject);
        }
    }
}
