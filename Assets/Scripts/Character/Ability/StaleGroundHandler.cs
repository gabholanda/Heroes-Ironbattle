using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Stale Ground Handler", menuName = "ScriptableObjects/Ability Handlers/New Stale Ground Handler")]
public class StaleGroundHandler : AbilityHandler
{
    public float offsetX;
    public float offsetY;
    public override void Initialize(GameObject player, Vector2 v2)
    {
        ability = prefab.GetComponent<Ability>();
        this.isCoolingDown = false;
        this.coRunner = player.GetComponent<CoroutineRunner>();
    }

    public override void Execute(GameObject player, Vector2 v2)
    {
        Transform playerTransform = player.GetComponent<PlayerController>().transform;
        Vector3 castingPoint = new Vector2(playerTransform.position.x - offsetX, playerTransform.position.y - offsetY);
        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (castingPoint.x,
                    castingPoint.y),
                    Quaternion.identity);
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        obj.GetComponent<Ability>().StartTimers();
    }
}
