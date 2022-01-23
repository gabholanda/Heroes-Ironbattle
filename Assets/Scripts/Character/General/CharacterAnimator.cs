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
    }
    public void SetAnimation(string animation, bool hasChild, bool useMain)
    {
        currentAnim = animation;
        if (useMain)
            animator.Play(currentAnim);
        if (hasChild)
            childAnimation.PlayAnimation(currentAnim);
    }

    public string GetCurrentAnimation()
    {
        return currentAnim;
    }
}
