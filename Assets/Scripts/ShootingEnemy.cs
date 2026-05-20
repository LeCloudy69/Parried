using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{
    // --- CONFIG ---
    [Header("Combat Settings")]
    public float attackRange = 15f; 
    public float timeBetweenShots = 1.5f; 

    [Header("Debug")]
    public bool showGizmos = true; // debug toggle 

    // --- REFERENCES ---
    [Header("Setup")]
    public GameObject bulletPrefab;
    public Transform firePoint; 
    private Transform player;
    private NavMeshAgent agent;

    // --- STATE MACHINE ---
    private enum State { Chasing, Attacking }
    private State currentState;
    
    // --- TIMERS ---
    private float shotTimer;

    // Animation
    [Header("Animation")]
    public Animator animator;

    // enemy counter
    public static int enemyCount = 0;

    void Start()
    {
        enemyCount++;
        agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();
        
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if(p != null) player = p.transform;
        
        currentState = State.Chasing;

        
    }

    void Update()
    {
        if (player == null) return; 

        if (animator != null)
        {
            // Get the speed 
            float speed = agent.velocity.magnitude;
            
            // Send it to the Blend Tree parameter named "Forward"
            animator.SetFloat("Forward", speed);
        }

        float distToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Chasing:
                HandleChasing(distToPlayer);
                break;
            case State.Attacking:
                HandleAttacking(distToPlayer);
                break;
        }
    }

    void HandleChasing(float distance)
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (distance <= attackRange)
        {
            currentState = State.Attacking;
        }
    }

    void HandleAttacking(float distance)
    {
        agent.isStopped = true; 
        
        // rotate towards player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // shooting logic
        shotTimer += Time.deltaTime;
        if (shotTimer >= timeBetweenShots)
        {
            Shoot();
            shotTimer = 0;
        }

        if (distance > attackRange * 1.2f) 
        {
            currentState = State.Chasing;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            
            // adjust vector3.up if it aims too low.
            Vector3 targetPos = player.position + Vector3.up * 1.4f; 
            
            Vector3 aimDir = (targetPos - firePoint.position).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(aimDir);

            // Instantiate bullet
            Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        }
    }

    void OnDestroy()
    {
        enemyCount--; // enemy dies
    }

    // --- VISUAL DEBUGGING ---
    private void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            // Draw the Attack Range sphere
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            
            // Draw a line to show where the enemy is trying to look (optional help)
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * attackRange);
        }
    }
}