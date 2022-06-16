using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash Handler", menuName = "ScriptableObjects/Ability Handlers/New Dash Handler")]
public class DashHandler : AbilityHandler
{
    public float dashDuration;
    public bool isDashing = false;
    public override void Initialize(GameObject player, Vector2 v2)
    {
        ability = prefab.GetComponent<Ability>();
        this.isCoolingDown = false;
        this.coRunner = player.GetComponent<CoroutineRunner>();
    }
    public override void Execute(GameObject g, Vector2 v2)
    {
        coRunner.Run(this.StartDashing());
    }

    public IEnumerator StartDashing()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
    }
}
