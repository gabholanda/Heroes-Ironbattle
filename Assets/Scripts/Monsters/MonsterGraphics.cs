using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGraphics : MonoBehaviour
{
    public Animator anim;
    public Transform gfxTransform;
    public Material material;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        material = GetComponentInChildren<SpriteRenderer>().material;
    }

}
