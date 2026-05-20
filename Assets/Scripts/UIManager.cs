using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public Image healthBarFill;
    public Health playerHealth;

    public Animator damageAnimator; 


    void Update()
    {
        if (playerHealth != null && healthBarFill != null)
        {
            // Calculate percentage (Current / Max)
            float fillValue = (float)playerHealth.currentHealth / playerHealth.maxHealth;
            
            // Update the bar
            healthBarFill.fillAmount = fillValue;
        }
    }

    public void OnPlayerHurt()
    {
        if(damageAnimator != null)
        {
            // The Animator handles the alpha fading now
            damageAnimator.SetTrigger("HurtFlash");
        }
    }
}