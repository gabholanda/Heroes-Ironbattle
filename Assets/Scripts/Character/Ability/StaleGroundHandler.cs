using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Stale Ground Handler", menuName = "ScriptableObjects/Ability Handlers/New Stale Ground Handler")]
public class StaleGroundHandler : AbilityHandler
{
    public float offsetX;
    public float offsetY;
    private Transform startPoint;
    public override void Initialize(GameObject player, Vector2 v2)
    {
        ability = prefab.GetComponent<Ability>();
        this.isCoolingDown = false;
        this.coRunner = player.GetComponent<CoroutineRunner>();
    }

    public override void Execute(GameObject player, Vector2 v2)
    {
        startPoint = player.GetComponent<PlayerController>().castingPoint.transform;
        Vector3 castingPoint = new Vector2(startPoint.position.x - (offsetX * player.transform.localScale.x), startPoint.position.y - offsetY);

        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (castingPoint.x,
                    castingPoint.y),
                    Quaternion.identity);
        obj.transform.localScale = new Vector3(player.transform.localScale.x, 1f, 1f);
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        obj.GetComponent<Ability>().StartTimers();
    }
}
