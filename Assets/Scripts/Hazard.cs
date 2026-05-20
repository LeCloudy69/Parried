using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage = 1;
    public bool instantKill = false;

    private void OnTriggerStay(Collider other)
    {
        // Check if the thing in the lava has Health
        Health targetHealth = other.GetComponent<Health>();
        
        if (targetHealth != null)
        {
            if (instantKill)
            {
                targetHealth.TakeDamage(1000);
            }
            else
            {
                // Deal damage over time (e.g., every frame)
                targetHealth.TakeDamage(damage);
            }
        }
    }
}