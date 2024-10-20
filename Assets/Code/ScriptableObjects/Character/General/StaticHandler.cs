using UnityEngine;
[CreateAssetMenu(fileName = "Static Handler", menuName = "ScriptableObjects/Ability Handlers/New Static Handler")]
public class StaticHandler : AbilityHandler
{
    public override void Initialize(GameObject caster)
    {
        this.isCoolingDown = false;
        this.coRunner = caster.GetComponentInChildren<CoroutineRunner>();
    }

    public override Ability Execute(GameObject caster, Vector2 v2)
    {
        Vector3 pos;
        pos = caster.transform.position;
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        this.isCoolingDown = true;
        Ability ability = obj.GetComponent<Ability>();
        ability.SetupAbility(caster);
        return ability;
    }
}
