using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private Animator anim;
    private bool dead;

    [Header("Invincibility Frames")]
    [Tooltip("Duration of invincibility after taking damage (in seconds).")]
    [SerializeField] private float invincibilityDuration;
    private float invincibilityTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void Update()
    {
        // Update the invincibility timer
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;

            if (spriteRenderer != null)
            {
                float alpha = Mathf.Abs(Mathf.Sin(Time.time * 10));
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            }
        }
        else
        {
            // Reset the sprite's color when not invincible
            if (spriteRenderer != null)
            {
                spriteRenderer.color = originalColor;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (invincibilityTimer <= 0)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

            if (currentHealth > 0)
            {
                // Player is hurt
                anim.SetTrigger("hurt");
                invincibilityTimer = invincibilityDuration;
                Debug.Log("Player took damage, currentHealth: " + currentHealth);
            }
            else
            {
                // Player is dead
                if (!dead)
                {
                    dead = true;
                    GetComponent<PlayerMovement>().enabled = false;
                    // Trigger death animation
                    anim.SetTrigger("die");
                    // The respawn will now be handled by the animation event calling OnDeathAnimationComplete()
                }
            }
        }
    }

    // This method is called via an Animation Event at the end of your death animation
    public void OnDeathAnimationComplete()
    {
        Die();
    }

    public void Die()
    {
        Debug.Log("Die() called. Current Health: " + currentHealth);
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.RespawnPlayer();
        }
        else
        {
            Debug.LogError("No GameManager Found: Unable to respawn player");
        }
    }

    public void ResetPlayer(Vector3 spawnPosition)
    {
        // Reset health and position
        currentHealth = startingHealth;
        transform.position = spawnPosition;

        // Reset physics
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reset Animator state: Rebind resets all animator states to the default
        anim.Rebind();
        anim.Update(0f);

        // Mark the player as alive
        dead = false;

        // Re-enable player movement
        GetComponent<PlayerMovement>().enabled = true;
    }


    public void ResetPlayerStats(float newHealthValue, Vector3 spawnPosition)
    {
        currentHealth = newHealthValue;

        transform.position = spawnPosition;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
