using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPointR;
    public Transform attackPointL;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 20;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        } 
    }

    private void Attack()
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
            if(enemy.name == "Skeleton")
            {
                enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
            }
            else if(enemy.name == "BurningGhoul")
            {
                enemy.GetComponent<BurningGhoul>().TakeDamage(attackDamage);
            }

        }
    }

}
