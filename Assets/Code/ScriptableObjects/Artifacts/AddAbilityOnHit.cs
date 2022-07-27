using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAbilityOnHit : MonoBehaviour
{
    public AbilityHandler handler;
    public Vector2 position;
    public void AddToAbilityEvent(GameObject caster)
    {
        AbilityHandler[] handlers = caster.GetComponent<CharacterStateMachine>().handlers;

        for (int i = 0; i < handlers.Length; i++)
        {
            //handlers[i].onHitEvent += Test;
        }
    }

    public void Test(Collider2D col, GameObject test)
    {

    }
}
