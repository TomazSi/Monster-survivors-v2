using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    //stats
    public int maxHealth;
    public int health;
    public float healthRegen; //??
    public float speed;
    public int damage = 1;
    public float attackSpeed = 1f;
    public int level = 0;
    public int XP = 0;

    public Transform basicAttackPoint = null;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    public void TakeDamage(int change)
    {
        health -= change;
        if (health <= 0)
            Die();
    }
    public void Heal(int change)
    {
        health += change;
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthRegen=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BasicAttack();
        }
    }
    private void BasicAttack()
    {
        int attackDamage = damage * 5;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(basicAttackPoint.position,attackRange,enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<ZombieController>().enemyAttributes.ChangeHealth(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (basicAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(basicAttackPoint.position, attackRange);
    }
    private void Die()
    {
        Debug.Log("END");
    }
}
