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
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void ChangeHealth(int change)
    {
        health -= change;
        Debug.Log("Monster took " + change + " damage "+ health);
        if (health <= 0)
            Die();
    }
    public void Die()
    {
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
