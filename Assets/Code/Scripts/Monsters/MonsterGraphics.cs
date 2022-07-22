using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGraphics : MonoBehaviour
{
    public Animator anim;
    public Transform gfxTransform;
    public Material material;
    public Animator[] children;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        material = GetComponentInChildren<SpriteRenderer>().material;
        children = GetComponentsInChildren<Animator>();
    }

    public void PlayChildrenAnimations(string animation)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].Play(animation);
        }
    }

}
