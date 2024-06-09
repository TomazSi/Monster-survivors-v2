using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public GameObject PauseMenu;

    //stats
    private int maxHealth = 12;
    public int health;
    public float speed;
    public int damage = 1;
    public float attackSpeed = 1f;
    public int level = 0;
    public int Level { get; private set; } = 1;
    public int CurrentXP { get; private set; } = 0;
    public int neededXP { get; private set; } = 100;

    [SerializeField] HealthBar healthBar;

    public Transform basicAttackPoint = null;
    public float attackRange = 2f;
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
        FindObjectOfType<EndGameManager>().PlayerDied();
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

    // This method is called to add XP
    public void AddExperience(int xpGained)
    {
        CurrentXP += xpGained;
        Debug.Log($"Gained {xpGained} XP. Total XP: {CurrentXP}");
        CheckLevelUp();
    }

    // Check if player should level up
    private void CheckLevelUp()
    {
        bool leveledUp = false;
        while (CurrentXP >= neededXP)
        {
            CurrentXP -= neededXP;
            Level++;
            leveledUp = true; // Flag that we have leveled up at least once
            Debug.Log($"Leveled up to {Level}!");

            neededXP = CalculateXPForNextLevel(Level);
        }

        if (leveledUp) // If the player leveled up at least once, then show the level-up menu
        {
            FindObjectOfType<LevelUpManager>().ShowLevelUpMenu(); // Show the level-up UI only once after all level-ups are processed
        }
    }


    // Calculate XP required for next level
    private int CalculateXPForNextLevel(int level)
    {
        return Mathf.FloorToInt(100 * Mathf.Pow(1.5f, level - 1));
    }

    public void IncreaseHealth()
    {
        maxHealth += 5;  // Adjust value as needed
        health = maxHealth;
        Debug.Log("Health upgraded!");
        FindObjectOfType<LevelUpManager>().HideLevelUpMenu();
    }

    public void IncreaseSpeed()
    {
        speed += 1;
        Debug.Log("Speed upgraded!");
        FindObjectOfType<LevelUpManager>().HideLevelUpMenu();
    }

    public void IncreaseStrength()
    {
        damage += 1;
        Debug.Log("Strength upgraded!");
        FindObjectOfType<LevelUpManager>().HideLevelUpMenu();
    }

    public void IncreaseAttackSpeed()
    {
        attackSpeed += 0.5f;
        Debug.Log("Attack Speed upgraded!");
        FindObjectOfType<LevelUpManager>().HideLevelUpMenu();
    }

    public void IncreaseRange()
    {
        attackRange += 0.2f;
        Debug.Log("Attack range upgraded!");
        FindObjectOfType<LevelUpManager>().HideLevelUpMenu();
    }
}
