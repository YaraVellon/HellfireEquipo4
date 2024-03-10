using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePointsPotion : MonoBehaviour
{
    private AudioSource audioSource;
    private GameManager gameManager;
    private SpriteRenderer spRd;
    private bool sumado;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        spRd = GetComponent<SpriteRenderer>();
        sumado = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && sumado == false)
        {
            sumado = true;

            audioSource.Play();

            Color color = spRd.color;
            color.a = 0.0f;
            spRd.color = color;

            gameManager.subirPuntos(30);
            gameManager.subirTiempo(5);
            Destroy(gameObject, 0.5f);
        }
    }
}
