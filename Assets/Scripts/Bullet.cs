using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;
    public int damage = 1; // Damage to deal on hit
    public bool isHostile = true; 

    void Start()
    {
        // Destroy bullet after 5 seconds to prevent lag
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move forward constantly
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // --- PARRY LOGIC ---
    public void Reflect(Vector3 newDirection)
    {
        isHostile = false; // Now it kills enemies
        transform.forward = newDirection; // Flip direction
        speed *= 2f; // Reward: It goes faster!
        damage *= 3; // Reward: It does more damage!
        
        // Visual Feedback: Change color to Blue
        GetComponent<Renderer>().material.color = Color.blue; 
        
        // Reset lifetime so it doesn't vanish mid-air
        CancelInvoke(); 
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. HIT PLAYER or HIT ENEMY (Reflected)
        // We can combine these checks because "Hostility" determines who gets hit
        if ((isHostile && other.CompareTag("Player")) || (!isHostile && other.CompareTag("Enemy")))
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
            
            // Bullets always destroy themselves on impact
            Destroy(gameObject);
        }
        // 3. HIT WALL (Ignore Triggers)
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}