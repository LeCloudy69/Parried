using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    [Header("Settings")]
    public Transform cam; 
    public bool isDefensiveOnly = false; // CHECK THIS for the Parry Box!

    private void OnTriggerEnter(Collider other)
    {
        // 1. PARRY (Reflect Bullet) - This works for BOTH modes
        if (other.CompareTag("EnemyProjectile"))
        {
            Bullet b = other.GetComponent<Bullet>();
            if (b != null)
            {
                if(cam != null) b.Reflect(cam.forward);
                else b.Reflect(transform.root.forward);
            }
        }
        // 2. KILL ENEMY - Only if we are NOT in defensive mode
        else if (other.CompareTag("Enemy"))
        {
            // If this is the Parry Shield, ignore enemies (don't kill them)
            if (isDefensiveOnly) return; 

            Debug.Log("Enemy Sliced!");
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
            }
            else
            {
                Destroy(other.gameObject); 
            }
        }
    }
}