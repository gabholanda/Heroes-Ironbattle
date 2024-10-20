using UnityEngine;

public class TeleporterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    public void SpawnTeleporter()
    {
        towerPrefab.transform.SetPositionAndRotation(gameObject.transform.position + new Vector3(0, -7f), Quaternion.identity);
        towerPrefab.SetActive(true);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
