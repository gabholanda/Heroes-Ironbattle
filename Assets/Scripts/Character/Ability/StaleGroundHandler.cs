using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Stale Ground Handler", menuName = "ScriptableObjects/Ability Handlers/New Stale Ground Handler")]
public class StaleGroundHandler : AbilityHandler
{
    private Transform startPoint;
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
        Transform castingPoint = player.GetComponent<PlayerController>().transform;
        startPoint.transform.position = new Vector2(castingPoint.position.x - offsetX, castingPoint.position.y - offsetY);
        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (startPoint.transform.position.x,
                    startPoint.transform.position.y),
                    Quaternion.identity);
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        obj.GetComponent<Ability>().StartTimers();
    }
}
