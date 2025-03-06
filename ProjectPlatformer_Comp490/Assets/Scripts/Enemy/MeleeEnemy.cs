using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageTaken;


    [Header("Enemy Death Sound")]
    [SerializeField] private AudioClip deathSound;

    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerHealth != null && playerRb != null)
            {
                // Determine the collision point and normal
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 contactPoint = contact.point;
                Vector2 contactNormal = contact.normal;

                // Calculate relative positions
                float playerBottom = collision.gameObject.transform.position.y -
                                     collision.gameObject.GetComponent<Collider2D>().bounds.extents.y;
                float enemyTop = transform.position.y + GetComponent<Collider2D>().bounds.extents.y;

                // Check if the player is above the enemy and moving down
                if (playerBottom > enemyTop && playerRb.velocity.y <= 0)
                {
                    // Player is jumping on the enemy's head
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damageTaken);
                    }

                    // Make the player bounce upwards
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 10f); // Adjust bounce force as needed
                    SoundManager.instance.PlaySound(deathSound);
                    // Optional: Add score or play sound effect
                }
                else
                {
                    // Player collides from the side or below; apply damage
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }
}
