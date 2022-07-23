using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectiles : MonoBehaviour
{
    public ProjectileHandler handler;
    public List<ProjectileHandler> handlers;
    [Header("Must provide directions")]
    public List<Vector2> directions;
    public Vector3 offsets;
    private void Awake()
    {
        handlers = new List<ProjectileHandler>();
        for (int i = 0; i < directions.Count; i++)
        {
            handlers.Add(ScriptableObject.CreateInstance<ProjectileHandler>());
            handlers[i].isCoolingDown = false;
            handlers[i].limiter = handler.limiter;
            handlers[i].prefab = handler.prefab;
            handlers[i].projectileSpeed = handler.projectileSpeed;
            handlers[i].SetAbilityData(handler.GetAbilityData());
            handlers[i].SetAbility(handler.GetAbility());
            handlers[i].dir = directions[i];
        }

        StartCoroutine(StartSpawning());
    }

    public IEnumerator StartSpawning()
    {
        while (this != null)
        {
            yield return new WaitForSeconds(handler.GetAbilityData().cooldownDuration);
            handlers.ForEach((h) =>
            {
                Ability ability = h.Execute(transform.parent.gameObject, h.dir);
                ability.handler = h;
                ability.AfterSetup();
            });
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void Add(GameObject target)
    {
        GameObject obj = Instantiate(gameObject, target.transform);
        obj.transform.position += offsets;
    }
}
