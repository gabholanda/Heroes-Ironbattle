using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spinning Handler", menuName = "ScriptableObjects/Ability Handlers/New Spinning Handler")]
public class SpinningHandler : AbilityHandler
{
    public ParticleSystem ps;
    private Transform initalSpawnPoint;
    public float dir;
    public float offset;
    public float angle;
    public override void Initialize(GameObject player, Vector2 v2)
    {
        ability = prefab.GetComponent<Ability>();
        ability.caster = player;
        this.coRunner = player.GetComponent<CoroutineRunner>();
        initalSpawnPoint = player.transform;
        this.isCoolingDown = false;
    }
    public override void Execute(GameObject player, Vector2 v2)
    {
        initalSpawnPoint = player.GetComponent<PlayerController>().transform;
        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (initalSpawnPoint.transform.position.x,
                    initalSpawnPoint.transform.position.y - offset),
                    Quaternion.AngleAxis(angle, Vector3.forward));
        obj.transform.SetParent(initalSpawnPoint);
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        obj.GetComponent<Ability>().StartTimers();
    }

}
