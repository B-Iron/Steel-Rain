using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] float bulletDeletionY = -12f;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Delete the projectile if it falls off the map
        if (transform.position.y <= bulletDeletionY)
        {
            Debug.Log("Projectile below deletion threshold.");
            DestroyProjectile();
        }
        
    }

   // Initialize projectile velocity based on angle and power
    public void Initialize(float angle, float power)
    {
        float angleInRadians = angle * Mathf.Deg2Rad;
        Vector2 launchVelocity = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * power;
        rb2d.velocity = launchVelocity;
    }

    private void DestroyProjectile()
    {
        Debug.Log("Destroying projectile");
        rb2d.velocity = Vector2.zero;
        Destroy(gameObject);

        // Reset camera to follow player
        FindObjectOfType<CameraFollow>().ResetToPlayer();
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("EnemyTank"))
    //     {
    //         // Subtract damage from the enemy tank's health
    //         Tank enemyTank = collision.GetComponent<Tank>();
    //         if (enemyTank != null)
    //         {
    //             enemyTank.TakeDamage(damage);
    //         }
    //         DestroyProjectile(); // Destroy projectile on impact
    //     }
    // }


}
