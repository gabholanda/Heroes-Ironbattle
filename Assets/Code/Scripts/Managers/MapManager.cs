using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
public enum MapType
{
    Florest,
    Dungeon
}
public class MapManager : MonoBehaviour
{
    [SerializeField]
    MapGenerator mapGenerator;
    public GameObject player;
    [SerializeField]
    private List<GameObject> monsters;
    public List<GameObject> monstersToSpawn;
    public List<MapItems> availableMapItems;
    public Tilemap floorTilemap;
    public Tilemap middleTilemap;
    public Tilemap topTilemap;
    public int minSize, maxSize;
    public AstarPath astarPath;

    public GameEvent OnFinishGeneratingMap;
    [SerializeField]
    private FloatVariable waves;
    private void Awake()
    {
        waves.Value = 0;
        //StartCoroutine(Automap());
        StartGeneratingMap();
    }

    public void Restart()
    {
        waves.Value = 0;
        StartGeneratingMap();
    }

    public void StartGeneratingMap()
    {
        SetTilemaps()
        .PickGenerator()
        .GenerateMap()
        .SpawnPlayer()
        .ClearEnemies()
        .SpawnEnemies()
        .Pathfind();

        OnFinishGeneratingMap?.Raise();

        waves.Value++;

        if (waves.Value % 3 == 0)
        {
            Debug.Log("Boss time!");
        }
    }

    IEnumerator Automap()
    {
        while (true)
        {
            StartGeneratingMap();
            yield return new WaitForSeconds(10f);
        }
    }

    private MapManager SetTilemaps()
    {
        mapGenerator.floorTilemap = floorTilemap;
        mapGenerator.middleTilemap = middleTilemap;
        mapGenerator.topTilemap = topTilemap;
        return this;
    }

    private MapManager PickGenerator()
    {
        switch (mapGenerator.mapItems.type)
        {
            case MapType.Florest:
                mapGenerator = (FlorestGenerator)mapGenerator;
                mapGenerator.mapItems = availableMapItems.Find((mapItems) => mapItems.type == MapType.Florest);
                break;
            case MapType.Dungeon:
                break;
        }
        return this;
    }

    private MapManager GenerateMap()
    {
        mapGenerator
          .ClearMap()
          .SetMaxCoords(minSize, maxSize)
          .SetCoords()
          .SetPlayerSpawnPoint()
          .SetEnemiesSpawnPoints()
          .FillMap()
          .ClearCoords();
        return this;
    }

    private MapManager SpawnPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = mapGenerator.playerSpawnPoint;
            }
        }
        else
        {
            Instantiate(player, new Vector3(mapGenerator.playerSpawnPoint.x, mapGenerator.playerSpawnPoint.y), Quaternion.identity);
        }
        return this;
    }

    public MapManager ClearEnemies()
    {
        monsters.ForEach(e => Destroy(e));
        return this;
    }

    public MapManager SpawnEnemies()
    {
        int enemiesQty = Random.Range(5, 15);
        for (int i = 0; i < enemiesQty; i++)
        {
            monsters.Add(Instantiate(GetRandomEnemy(), GetRandomEnemySpawnPoint(), Quaternion.identity));
        }
        return this;
    }
    private GameObject GetRandomEnemy()
    {
        return monstersToSpawn[Random.Range(0, monstersToSpawn.Count)];
    }
    private Vector3 GetRandomEnemySpawnPoint()
    {
        return mapGenerator.enemiesSpawnPoints[Random.Range(0, mapGenerator.enemiesSpawnPoints.Count - 1)];
    }

    private void Pathfind()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        yield return new WaitForSeconds(0.5f);
        astarPath.Scan();
    }
}
