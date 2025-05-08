using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Awake()
    {
        // Only assign if not already set in the Inspector

        if (playerHealth == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerHealth = p.GetComponent<Health>();
            else Debug.LogWarning("HealthBar: Player tag not found");
        }

        if (totalhealthBar == null)
        {
            var t = GameObject.Find("HealthBarTotal");
            if (t != null) totalhealthBar = t.GetComponent<Image>();
            else Debug.LogWarning("HealthBar: 'HealthBarTotal' not found");
        }

        if (currenthealthBar == null)
        {
            var c = GameObject.Find("HealthBarCurrent");
            if (c != null) currenthealthBar = c.GetComponent<Image>();
            else Debug.LogWarning("HealthBar: 'HealthBarCurrent' not found");
        }
    }

    private void Start()
    {
        // Your existing logic
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }

    private void Update()
    {
        // Your existing logic
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }
}
