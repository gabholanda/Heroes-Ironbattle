using UnityEngine;
[CreateAssetMenu(fileName = "Projectile Handler", menuName = "ScriptableObjects/Ability Handlers/New Projectile Handler")]
public class ProjectileHandler : AbilityHandler
{
    [Header("Projectile Attributes")]
    private Transform startPoint;
    public float projectileSpeed;
    public Vector3 dir;
    public float limiter;

    public override void Initialize(GameObject caster)
    {
        this.isCoolingDown = false;
        this.coRunner = caster.GetComponent<CoroutineRunner>();
    }
    public override void Execute(GameObject caster, Vector2 v2)
    {
        startPoint = caster.GetComponent<PlayerStateMachine>().castingPoint.transform;
        dir = SetDirection(v2, caster.transform.localScale, limiter);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (startPoint.transform.position.x,
                    startPoint.transform.position.y),
                    Quaternion.AngleAxis(angle, Vector3.forward));
        Ability ability = obj.GetComponent<Ability>();
        ability.caster = caster;
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        ability.StartTimers();
    }

    private Vector2 SetDirection(Vector2 pos, Vector2 playerScale, float limiter)
    {
        pos = ((Vector3)pos - startPoint.transform.position).normalized;
        if (pos.y > limiter)
        {
            pos.y = limiter;
        }
        else if (pos.y < -limiter)
        {
            pos.y = -limiter;
        }
        pos.x = playerScale.x;
        return pos;
    }
}
