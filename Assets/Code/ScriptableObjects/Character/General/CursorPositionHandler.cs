using UnityEngine;

[CreateAssetMenu(fileName = "Cursor Position Handler", menuName = "ScriptableObjects/Ability Handlers/New Cursor Position Handler")]
public class CursorPositionHandler : AbilityHandler
{
    public float offsetX;
    public float offsetY;
    public override void Initialize(GameObject player)
    {
        this.isCoolingDown = false;
        this.coRunner = player.GetComponent<CoroutineRunner>();
    }

    public override Ability Execute(GameObject caster, Vector2 v2)
    {
        Vector3 positionToWorld = v2;
        positionToWorld.z = 0;
        positionToWorld.x -= offsetX;
        positionToWorld.y -= offsetY;
        GameObject obj = Instantiate(prefab,
                    positionToWorld,
                    Quaternion.identity);
        Ability ability = obj.GetComponent<Ability>();
        ability.SetupAbility(caster);
        return ability;
    }
}
