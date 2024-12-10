using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has fallen into the death zone
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
        }
    }
}
