using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Cursor Position Handler", menuName = "ScriptableObjects/Ability Handlers/New Cursor Position Handler")]
public class CursorPositionHandler : AbilityHandler
{
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
        // TODO: fazer isso em um script de utility;
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f;
        Vector3 positionToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        positionToWorld.z = 0;
        positionToWorld.x -= offsetX;
        positionToWorld.y -= offsetY;
        GameObject obj = Instantiate(prefab,
                    positionToWorld,
                    Quaternion.identity);
        this.isCoolingDown = true;
        coRunner.Run(this.StartCooldown());
        obj.GetComponent<Ability>().StartTimers();
    }
}
