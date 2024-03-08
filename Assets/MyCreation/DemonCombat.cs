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

    // Variable para asignar el ataque al bot�n
    public Button botonAtaque;

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
            }
            /*if(enemy.name == "Skeleton")
            {
                enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
            }
            else if(enemy.name == "BurningGhoul")
            {
                Debug.Log("Hitted");
                enemy.GetComponent<BurningGhoul>().TakeDamage(attackDamage);
            }*/

        }
    }

}
