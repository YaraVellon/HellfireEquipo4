using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPointR;
    public Transform attackPointL;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage;
    private GameManager gameManager;

    // Variable para asignar el ataque al botón
    public Button botonAtaque;

    //Tiempo que tarda en poder atacar de nuevo
    public float attackRate = 2f;
    float nextAttackTime = 0f;


    void Start()
    {
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManager>();
        attackDamage = gameManager.getAttackDamage();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");

            Collider2D[] hitEnemies;

            if (GetComponent<DemonBehaviour>().dir)
            {
                hitEnemies = Physics2D.OverlapCircleAll(attackPointL.position, attackRange, enemyLayers);
            }
            else
            {
                hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayers);
            }

            foreach (Collider2D enemy in hitEnemies)
            {
                switch (enemy.name)
                {
                    case "Skeleton":
                        enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
                        break;
                    case "BurningGhoul":
                        enemy.GetComponent<BurningGhoul>().TakeDamage(attackDamage);
                        break;
                    case "ClothedSkeleton":
                        enemy.GetComponent<ClothedSkeleton>().TakeDamage(attackDamage);
                        break;
                    case "GhostHalo":
                        enemy.GetComponent<GhostHalo>().TakeDamage(attackDamage);
                        break;
                    case "Wizard":
                        enemy.GetComponent<Wizard>().TakeDamage(attackDamage);
                        break;
                    case "DemonBoss":
                        enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                        break;
                    case "DemonBossHard":
                        enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                        break;
                }

            }



            nextAttackTime = Time.time + 1f / attackRate;
        } 
    }

}
