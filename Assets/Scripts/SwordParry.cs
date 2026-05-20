using UnityEngine;

public class SwordParry : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Transform cam;

    [Header("Hitboxes")]
    public Collider swordHitbox;   // Drag the SWORD collider here (RightHand child)
    public Collider parryHitbox;   // Drag the PARRY box here (PlayerCam child)

    [Header("Inputs")]
    public KeyCode attackKey = KeyCode.Mouse0; 
    public KeyCode parryKey = KeyCode.F;     

    // Hand IK for parry pose
    public SimpleHandIK handIK;  

    private bool isBusy = false; 
    
    // We need to remember WHICH move we are doing so we open the right box
    private bool isParryingAction = false; 

    void Start()
    {
        // Safety: Ensure both are off at start
        if (swordHitbox != null) swordHitbox.enabled = false;
        if (parryHitbox != null) parryHitbox.enabled = false;
    }

    void Update()
    {
        // 1. SLASH ATTACK (Left Click)
        if (Input.GetKeyDown(attackKey) && !isBusy)
        {
            isParryingAction = false; //  SLASHING
            PerformAction("Attack");
        }

        // 2. PARRY/BLOCK (F Key)
        if (Input.GetKeyDown(parryKey) && !isBusy)
        {
            isParryingAction = true; // PARRYING
            PerformAction("Parry");
        }
    }

    void PerformAction(string triggerName)
    {
        isBusy = true;
        if (animator != null) animator.SetTrigger(triggerName);

        if (triggerName == "Parry" && handIK != null)
        {
            handIK.targetWeight = 1f; // hand wasnt on blade properly so we IK it.
        }
    }

    // --- ANIMATION EVENTS ---

    public void OpenHitbox()
    {
        if (isParryingAction)
        {
            // If we pressed F, open the parry window
            if (parryHitbox != null) parryHitbox.enabled = true;
        }
        else
        {
            // If we Clicked, open the sword hitbox
            if (swordHitbox != null) swordHitbox.enabled = true;
        }
    }

    public void CloseHitbox()
    {
        // turn BOTH off to be safe and clean
        if (swordHitbox != null) swordHitbox.enabled = false;
        if (parryHitbox != null) parryHitbox.enabled = false;
        
        if (handIK != null) handIK.targetWeight = 0f;
        isBusy = false; 
    }
}