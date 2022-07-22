using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Stale Ground Handler", menuName = "ScriptableObjects/Ability Handlers/New Stale Ground Handler")]
public class StaleGroundHandler : AbilityHandler
{
    public float offsetX;
    public float offsetY;
    private Transform startPoint;
    public override void Initialize(GameObject caster)
    {
        this.isCoolingDown = false;
        this.coRunner = caster.GetComponent<CoroutineRunner>();
    }

    public override void Execute(GameObject caster, Vector2 v2)
    {
        startPoint = caster.GetComponent<CharacterStateMachine>().castingPoint.transform;
        Vector3 castingPoint = new Vector2(startPoint.position.x - (offsetX * caster.transform.localScale.x), startPoint.position.y - offsetY);

        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (castingPoint.x,
                    castingPoint.y),
                    Quaternion.identity);
        obj.transform.localScale = new Vector3(caster.transform.localScale.x, 1f, 1f);
        Ability ability = obj.GetComponent<Ability>();
        ability.SetupAbility(caster);
    }
}
