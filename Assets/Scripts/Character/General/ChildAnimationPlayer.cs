using UnityEngine;

public class ChildAnimationPlayer : MonoBehaviour
{
    [SerializeField]
    private Animation anim;
    private bool isInterrupted = false;
    public string prefix;
    // Start is called before the first frame update
    void Start()
    {
        anim = anim.GetComponent<Animation>();
    }

    public void PlayAnimation(string animationName, bool _isNotInterruptable, bool _canInterrupt)
    {
        if (isInterrupted && !_canInterrupt)
            return;

        if (_canInterrupt && anim.IsPlaying(prefix + animationName))
        {
            anim.Rewind(prefix + animationName);
        }
        else
        {
            anim.Play(prefix + animationName);
        }
        if (_isNotInterruptable)
        {
            isInterrupted = true;
        }
    }

    public void HasFinished()
    {
        isInterrupted = false;
        anim.Play(prefix + "Idle");
    }
}
