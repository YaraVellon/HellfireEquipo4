using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipReverse : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float posicionXAnterior;

    // Start is called before the first frame update
    void Start()
    {
        posicionXAnterior = transform.position.x;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (sprite.name)
        {
            case "Skeleton":
                GetComponent<Skeleton>().dir = !(posicionXAnterior < transform.position.x);
                break;
            case "BurningGhoul":
                GetComponent<BurningGhoul>().dir = !(posicionXAnterior < transform.position.x);
                break;
            case "ClothedSkeleton":
                GetComponent<ClothedSkeleton>().dir = !(posicionXAnterior < transform.position.x);
                break;
            case "GhostHalo":
                GetComponent<GhostHalo>().dir = !(posicionXAnterior < transform.position.x);
                break;
            case "Wizard":
                GetComponent<Wizard>().dir = !(posicionXAnterior < transform.position.x);
                break;
            case "DemonBoss":
                GetComponent<Boss>().dir = !(posicionXAnterior < transform.position.x);
                break;
            case "DemonBossHard":
                GetComponent<Boss>().dir = !(posicionXAnterior < transform.position.x);
                break;
        }

        sprite.flipX = posicionXAnterior > transform.position.x;
        posicionXAnterior = transform.position.x;
    }
}
