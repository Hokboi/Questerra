using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health for the player
    private float currentHealth; // Current health for the player

    [SerializeField] private Slider healthBar; // Reference to a UI Slider for health

    void Start()
    {
        currentHealth = maxHealth; // Initialize health at start
        UpdateHealthBar(); // Initialize the health bar on start
    }

    void Update()
    {
        // Temporary for testing - simulate taking damage
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to take damage
        {
            TakeDamage(10f); // Example damage amount (adjust as necessary)
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce health by damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health doesn't go below 0

        if (currentHealth <= 0f)
        {
            Die(); // Call die method if health reaches zero
        }

        UpdateHealthBar(); // Update the health bar UI
        Debug.Log("Current Health: " + currentHealth); // Log current health for debugging
    }

    public void Heal(float amount)
    {
        currentHealth += amount; // Heal the player
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health does not exceed maxHealth

        UpdateHealthBar(); // Update the health bar UI
        Debug.Log("Current Health: " + currentHealth); // Log current health for debugging
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth; // Normalize health for the slider
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!"); // Handle death logic here
        // You can disable player controls, play death animation, etc.
        gameObject.SetActive(false); // Hide player for demonstration (replace with your death logic)
    }
}