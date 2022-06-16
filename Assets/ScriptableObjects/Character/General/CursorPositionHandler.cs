using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Cursor Position Handler", menuName = "ScriptableObjects/Ability Handlers/New Cursor Position Handler")]
public class CursorPositionHandler : AbilityHandler
{
    public float offsetX;
    public float offsetY;
    public override void Initialize(GameObject player, Vector2 v2)
    {
        ability = prefab.GetComponent<Ability>();
        this.isCoolingDown = false;
        this.coRunner = player.GetComponent<CoroutineRunner>();
    }

    public override void Execute(GameObject caster, Vector2 v2)
    {
        Vector3 positionToWorld = v2;
        positionToWorld.z = 0;
        positionToWorld.x -= offsetX;
        positionToWorld.y -= offsetY;
        GameObject obj = Instantiate(prefab,
                    positionToWorld,
                    Quaternion.identity);
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        Ability ability = obj.GetComponent<Ability>();
        ability.caster = caster;
        ability.StartTimers();
    }
}
