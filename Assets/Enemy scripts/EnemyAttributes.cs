using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int speed;
    public int damage = 5;
    public float attackSpeed = 1f;
    public float currentAttack = 0f;
    public int XPValue = 50;

    [SerializeField] HealthBar healthBar;

    private Animator animator;
    public PlayerAttributes playerAttributes;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        if (playerAttributes == null)
        {
            playerAttributes = FindObjectOfType<PlayerAttributes>();
            if (playerAttributes == null)
            {
                Debug.LogError("PlayerAttributes not found in the scene.");
            }
        }
    }

    public void ChangeHealth(int change)
    {
        animator.SetBool("Hit", true);
        health -= change;
        healthBar.SetHealth(health, maxHealth);
        Debug.Log("Monster took " + change + " damage " + health);
        if (health <= 0)
            Die();
        animator.SetBool("Hit", false);
    }
    public void Die()
    {
        int scaledXP = CalculateScaledXP();
        animator.SetBool("Dies", true);
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(10);
        playerAttributes.AddExperience(scaledXP);
        Destroy(gameObject);
    }
    public void SetStats(int maxHealth, int speed, int damage)
    {
        this.maxHealth = maxHealth;
        this.speed = speed;
        this.damage = damage;
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }
    public void Attack()
    {
        playerAttributes.TakeDamage(damage);
    }

    private int CalculateScaledXP()
    {
        float scalingFactor = 1.0f + (0.1f * (playerAttributes.Level - 1)); // Increase 10% per player level
        return Mathf.FloorToInt(XPValue * scalingFactor);
    }
}
