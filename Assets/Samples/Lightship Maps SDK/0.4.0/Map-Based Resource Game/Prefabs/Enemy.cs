using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followDistance = 5.0f; // Distance at which the enemy starts to follow the player
    public float followSpeed = 3.0f; // Speed while following the player
    public float idleMovementRadius = 2.0f; // Radius of random idle movement
    public float idleMovementSpeed = 1.0f; // Speed of idle movement

    private Vector3 idlePosition; // Store the next idle position
    private bool isFollowing = false; // To track if the enemy is following the player

    void Start()
    {
        SetNewIdlePosition(); // Set the initial idle position
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is within follow distance, move towards the player
            if (distanceToPlayer < followDistance)
            {
                isFollowing = true;
                FollowPlayer();
            }
            else
            {
                isFollowing = false;
                IdleMovement();
            }
        }
    }

    void FollowPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position += direction * followSpeed * Time.deltaTime;
    }

    void IdleMovement()
    {
        // If the enemy is close to the idle position, set a new idle position
        if (Vector3.Distance(transform.position, idlePosition) < 0.5f)
        {
            SetNewIdlePosition();
        }
        else
        {
            // Move to the idle position
            Vector3 direction = (idlePosition - transform.position).normalized;
            transform.position += direction * idleMovementSpeed * Time.deltaTime;
        }
    }

    void SetNewIdlePosition()
    {
        // Set a new random position within the defined radius
        idlePosition = new Vector3(
            transform.position.x + Random.Range(-idleMovementRadius, idleMovementRadius),
            transform.position.y,
            transform.position.z + Random.Range(-idleMovementRadius, idleMovementRadius)
        );
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
}