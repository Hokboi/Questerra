using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage dealt to the player
    public float attackRange = 1.5f; // Range within which the enemy can deal damage
    public float damageInterval = 1.0f; // Time in seconds between damage outputs

    private Transform player; // Reference to the player's transform
    private bool playerInRange; // To track if the player is in range for damage
    private float lastDamageTime; // Time at which damage was last dealt

    void Start()
    {
        // Find the player object by tag (assumes player has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Check the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < attackRange)
            {
                playerInRange = true;
                DealDamageToPlayer();
            }
            else
            {
                playerInRange = false;
            }
        }
    }

    void DealDamageToPlayer()
    {
        // Only deal damage if the player is in range and the time since the last damage is greater than the damage interval
        if (playerInRange && Time.time >= lastDamageTime + damageInterval)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Deal damage to the player
                lastDamageTime = Time.time; // Update the last damage time
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the enemy's trigger collider
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Player is in range
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Player has exited the range
        }
    }
}