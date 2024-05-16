using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    public Transform playerTransform;
    private Rigidbody2D rb;
    public int speed = 3;
    public int maxHealth = 10;
    public int damage = 2;
    private bool isCollidingWithPlayer = false;

    public EnemyAttributes enemyAttributes;
    // Start is called before the first frame update
    void Start()
    {
        enemyAttributes = GetComponent<EnemyAttributes>();
        enemyAttributes.SetStats(maxHealth, speed, damage);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!isCollidingWithPlayer)
        {
            Vector2 direction=(playerTransform.position - transform.position).normalized;
            rb.velocity = direction*speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void SetPlayerReference(Transform player)
    {
        playerTransform = player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isCollidingWithPlayer=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isCollidingWithPlayer=false;
        }
    }
}
