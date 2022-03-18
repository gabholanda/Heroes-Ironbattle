using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private ChildAnimationPlayer childAnimation;
    private string currentAnim;

    void Awake()
    {
        animator = GetComponent<Animator>();
        childAnimation = gameObject.GetComponentInChildren<ChildAnimationPlayer>();
        childAnimation.SetParentAnimator(this);
    }
    public void SetAnimation(string animation, bool hasChild, bool useMain, bool isNotInterruptible, bool canInterrupt)
    {
        if (useMain)
        {
            currentAnim = animation;
            animator.Play(currentAnim);
        }
        if (hasChild)
            childAnimation.PlayAnimation(animation, isNotInterruptible, canInterrupt);
    }

    public string GetCurrentAnimation()
    {
        return currentAnim;
    }
}
