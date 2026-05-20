using UnityEngine;

public class SimpleHandIK : MonoBehaviour
{
    public Animator animator;
    public Transform leftHandTarget; // Drag "LeftHandGrip" here
    
    [Range(0,1)]
    public float targetWeight = 0f; // 0 = Let Animation decide, 1 = Force Hand to Grip
    
    private float currentWeight = 0f;

    void Start()
    {
        if(animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Smoothly blend the IK on/off so the hand doesn't snap instantly
        currentWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * 10f);
    }

    // This runs AFTER the animation frame is calculated
    void OnAnimatorIK()
    {
        if(animator)
        {
            // force the hand (0 to 1)
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, currentWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, currentWeight);

            // WHERE to put the hand
            if(leftHandTarget != null) 
            {
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }
        }
    }
}