using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1Combat : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayers;

    int attackDamage = 25;
    int maxHealth = 100;
    int health;

    float attackRate = 2f;
    float nextAttackTime = 0f;


    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack(1);
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Attack(2);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack(int num)
    {
        animator.SetFloat("Attack", num);
        animator.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Hero2Combat>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Hurt anim

        // Hurt Sound

        if(health <=0 )
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
