using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
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
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit "+enemy.name);
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

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
