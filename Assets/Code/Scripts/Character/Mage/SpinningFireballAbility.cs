using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningFireballAbility : Ability
{
    public float spinSpeed;
    public float spinDir;
    public float yOffset;
    void Awake()
    {
        FireballAbility[] fireballs = GetComponentsInChildren<FireballAbility>();
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].caster = caster.transform.parent.gameObject;
            fireballs[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
    void Update()
    {
        Vector3 casterPosition = caster.transform.position;
        transform.position = new Vector3(casterPosition.x, casterPosition.y + yOffset, casterPosition.z);
        transform.Rotate(new Vector3(0, 0, spinDir), spinSpeed * Time.deltaTime);
    }
}
