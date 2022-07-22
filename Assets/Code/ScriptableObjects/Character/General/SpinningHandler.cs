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
    public override void Initialize(GameObject caster)
    {
        this.coRunner = caster.GetComponent<CoroutineRunner>();
        initialSpawnPoint = caster.transform;
        this.isCoolingDown = false;
    }
    public override void Execute(GameObject caster, Vector2 v2)
    {
        initialSpawnPoint = caster.GetComponent<CharacterStateMachine>().transform;
        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (initialSpawnPoint.transform.position.x,
                    initialSpawnPoint.transform.position.y - offset),
                    Quaternion.AngleAxis(angle, Vector3.forward));
        Ability ability = obj.GetComponent<Ability>();
        ability.SetupAbility(caster);
    }

}
