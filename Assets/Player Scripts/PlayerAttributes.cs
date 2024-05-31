using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public GameObject PauseMenu;

    //stats
    private int maxHealth = 12;
    public int health;
    public float healthRegen; //??
    public float speed;
    public int damage = 1;
    public float attackSpeed = 1f;
    public int level = 0;
    public int XP = 0;

    [SerializeField] HealthBar healthBar;

    public Transform basicAttackPoint = null;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    private Animator animator;

    public void TakeDamage(int change)
    {
        health -= change;
        healthBar.SetHealth(health, maxHealth);
        Debug.Log("Hit for: " + change);
        if (health <= 0)
            Die();
    }
    public void Heal(int change)
    {
        health += change;
        healthBar.SetHealth(health, maxHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        Debug.Log(health);
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        healthRegen =0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BasicAttack();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.activeInHierarchy)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void BasicAttack()
    {
        int attackDamage = damage * 5;
        animator.SetBool("BasicAttack", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(basicAttackPoint.position,attackRange,enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<ZombieController>().enemyAttributes.ChangeHealth(attackDamage);
        }
        animator.SetBool("BasicAttack", false);
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

    void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f; // This pauses the game
    }

    void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f; // This resumes the game
    }
}
