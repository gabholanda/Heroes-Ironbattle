﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Handler", menuName = "ScriptableObjects/Ability Handlers/New Dash Handler")]
public class DashHandler : AbilityHandler
{
    public float dashDuration;
    public bool isDashing = false;
    public override void Initialize(GameObject player)
    {
        this.isCoolingDown = false;
        this.coRunner = player.GetComponent<CoroutineRunner>();
    }
    public override Ability Execute(GameObject g, Vector2 v2)
    {
        coRunner.Run(this.StartDashing());
        return null;
    }

    public IEnumerator StartDashing()
    {
        isDashing = true;
        this.isCoolingDown = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        coRunner.Run(this.StartCooldown());
    }
}
