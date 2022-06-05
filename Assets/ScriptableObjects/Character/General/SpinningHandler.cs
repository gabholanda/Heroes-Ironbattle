using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spinning Handler", menuName = "ScriptableObjects/Ability Handlers/New Spinning Handler")]
public class SpinningHandler : AbilityHandler
{
    private Transform initialSpawnPoint;
    public float dir;
    public float offset;
    public float angle;
    public override void Initialize(GameObject player, Vector2 v2)
    {
        ability = prefab.GetComponent<Ability>();
        ability.caster = player;
        this.coRunner = player.GetComponent<CoroutineRunner>();
        initialSpawnPoint = player.transform;
        this.isCoolingDown = false;
    }
    public override void Execute(GameObject player, Vector2 v2)
    {
        initialSpawnPoint = player.GetComponent<PlayerStateMachine>().transform;
        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (initialSpawnPoint.transform.position.x,
                    initialSpawnPoint.transform.position.y - offset),
                    Quaternion.AngleAxis(angle, Vector3.forward));
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        Ability ability = obj.GetComponent<Ability>();
        ability.caster = player;
        ability.StartTimers();
    }

}
