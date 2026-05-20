using UnityEngine;
using UnityEngine.Events; 

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public int maxHealth = 3;
    public int currentHealth;
    
    // Invincibility (Mainly for Player)
    public float iFramesDuration = 1f;
    private bool isInvincible = false;

    [Header("Events")]
    // This lets us drag in functions later (like "Play Sound" or "Update Health Bar")
    public UnityEvent OnDeath;
    public UnityEvent OnDamage;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        // If we are invincible, ignore damage
        if (isInvincible) return;

        currentHealth -= amount;
        Debug.Log(gameObject.name + " took damage! HP: " + currentHealth);

        // Trigger 'OnDamage' event (Screen shake? Red flash?)
        OnDamage.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Activate iFrames
            if (iFramesDuration > 0) StartCoroutine(InvincibilityRoutine());
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " Died!");
        OnDeath.Invoke(); // Trigger death effects

        // If it's an enemy, destroy it. 
        // If it's the player, we might want to Restart Level instead of Destroying.
        if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Player"))
        {
            // Disable player control instead of destroying (so camera doesn't break)
            gameObject.SetActive(false); 
            // We will add "Game Over Screen" logic here later
        }
    }

    System.Collections.IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        // Optional: Flash model red/transparent here
        yield return new WaitForSeconds(iFramesDuration);
        isInvincible = false;
    }
}