using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int speed;
    public int damage = 1;
    public float attackSpeed = 1f;
    
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void ChangeHealth(int change)
    {
        animator.SetBool("Hit", true);
        health -= change;
        Debug.Log("Monster took " + change + " damage "+ health);
        if (health <= 0)
            Die();
        animator.SetBool("Hit", false);
    }
    public void Die()
    {
        animator.SetBool("Dies", true);
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(10); 
        Destroy(gameObject);
        Debug.Log("Monster died");
    }
    public void SetStats(int maxHealth, int speed, int damage)
    {
        this.maxHealth = maxHealth;
        this.speed = speed;
        this.damage = damage;
        if(this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }
}
