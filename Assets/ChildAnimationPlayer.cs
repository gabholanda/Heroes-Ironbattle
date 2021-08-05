using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnimationPlayer : MonoBehaviour
{
    [SerializeField]
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = anim.GetComponent<Animation>();
    }

    public void PlayAnimation(string name)
    {
        anim.Play("Staff-" + name);
    }
}
