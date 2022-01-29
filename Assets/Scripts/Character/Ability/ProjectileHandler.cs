using UnityEngine;
[CreateAssetMenu(fileName = "Projectile Handler", menuName = "ScriptableObjects/Ability Handlers/New Projectile Handler")]
public class ProjectileHandler : AbilityHandler
{
    public ParticleSystem ps;
    private Transform startPoint;
    public float projectileSpeed;
    public Vector3 dir;
    public float limiter;

    public override void Initialize(GameObject player, Vector2 v2)
    {
        this.isCoolingDown = false;
        this.coRunner = player.GetComponent<CoroutineRunner>();
    }
    public override void Execute(GameObject player, Vector2 v2)
    {
        startPoint = player.GetComponent<PlayerController>().castingPoint.transform;
        dir = SetDirection(v2, player.transform.localScale, limiter);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject obj = Instantiate(prefab,
                    new Vector3
                    (startPoint.transform.position.x,
                    startPoint.transform.position.y),
                    Quaternion.AngleAxis(angle, Vector3.forward));
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        obj.GetComponent<Ability>().StartTimers();
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

        if (playerScale.x > 0)
        {
            pos.x = 1;
        }
        else
        {
            pos.x = -1;
        }

        return pos;
    }
}
