using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towePrefab;

    public void SpawnTeleporter()
    {
        Instantiate(towePrefab, gameObject.transform.position + new Vector3(0, -7f), Quaternion.identity);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
