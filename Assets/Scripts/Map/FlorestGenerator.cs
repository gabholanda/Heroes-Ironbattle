using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "MapGenerator", menuName = "ScriptableObjects/Map/Map Generator")]
public class FlorestGenerator : MapGenerator
{
    public override MapGenerator SetMaxCoords(int minSize, int maxSize)
    {
        maxCoords = Random.Range(minSize, maxSize);
        return this;
    }

    public override MapGenerator SetCoords()
    {
        int y = 0;
        SetVerticalOuterCoords(y - 5, -5, 45);
        while (coords.Count < maxCoords)
        {
            int widthX = Random.Range(30, 40);
            int initialPosX = Random.Range(0, 5);
            int totalRow = widthX + initialPosX;
            SetHorizontalOutCoords(y, initialPosX, totalRow);
            // Gotta check because there is no way to know when the final row is happening
            bool notFinalRow = coords.Count + totalRow < maxCoords;
            for (int x = initialPosX; x < totalRow; x++)
            {
                if (IsGap())
                {
                    AddCoord(x, y, obstaclesCoords);
                    //x++;
                    //continue;
                }
                AddCoord(x, y, coords);
                if (IsWithinBoundary(x, initialPosX, totalRow, y, notFinalRow) && IsDecoration())
                    AddCoord(x, y, innerCoords);
            }
            y++;
        }
        SetVerticalOuterCoords(y, -5, 45);
        return this;
    }

    public override MapGenerator SetPlayerSpawnPoint()
    {
        bool setSpawnPoint = false;
        while (setSpawnPoint == false)
        {
            int rand = Random.Range(50, coords.Count - 50);
            for (int i = 0; i < obstaclesCoords.Count; i++)
            {
                if (NotObstacleCoordinate(coords[rand], obstaclesCoords[i]))
                {
                    playerSpawnPoint = middleTilemap.GetCellCenterWorld(new Vector3Int(coords[rand].x, coords[rand].y, 0));
                    setSpawnPoint = true;
                    break;
                }
            }
        }

        return this;
    }


    public override MapGenerator SetEnemiesSpawnPoints()
    {
        while (enemiesSpawnPoints.Count < 3)
        {
            int rand = Random.Range(30, coords.Count - 20);
            for (int i = 0; i < obstaclesCoords.Count; i++)
            {
                if (NotObstacleCoordinate(coords[rand], obstaclesCoords[i]))
                {
                    enemiesSpawnPoints.Add(middleTilemap.GetCellCenterWorld(new Vector3Int(coords[rand].x, coords[rand].y, 0)));
                    break;
                }
            }
        }
        return this;
    }

    public override MapGenerator FillMap()
    {
        coords.ForEach(ProcessCoordinates(floorTilemap, mapItems.bottomTiles, mapItems.bottomTiles.Length));
        innerCoords.ForEach(ProcessCoordinates(middleTilemap, mapItems.middleTiles, mapItems.middleTiles.Length));
        outerCoords.ForEach(ProcessCoordinates(floorTilemap, mapItems.outerBottomTiles, mapItems.outerBottomTiles.Length));
        outerCoords.ForEach(ProcessCoordinates(topTilemap, mapItems.outerTopTiles, mapItems.outerTopTiles.Length));
        obstaclesCoords.ForEach(ProcessCoordinates(topTilemap, mapItems.topTiles, mapItems.topTiles.Length));
        return this;
    }

    private Action<Coordinates> ProcessCoordinates(Tilemap tilemap, Tile[] tiles, int tilesLength)
    {
        return delegate (Coordinates coord)
        {
            tilemap.SetTile(new Vector3Int(coord.x, coord.y, 0), tiles[UnityEngine.Random.Range(0, tilesLength)]);
            //Debug.Log(coord.x + ":" + coord.y);
        };
    }

    private void SetHorizontalOutCoords(int y, int initialPosX, int totalRow)
    {
        // TODO: Optimize it so it wont generate unnecessary tiles
        for (int x = initialPosX - 20; x < initialPosX; x++)
        {
            AddCoord(x, y, outerCoords);
        }

        for (int x = totalRow; x < totalRow + 20; x++)
        {
            AddCoord(x, y, outerCoords);
        }
    }

    private void SetVerticalOuterCoords(int _y, int initialPosX, int totalRow)
    {
        for (int x = initialPosX - 5; x < totalRow + 5; x++)
        {
            for (int y = _y; y < _y + 5; y++)
            {
                AddCoord(x, y, outerCoords);
            }
        }
    }

    private bool IsWithinBoundary(int x, int initialPosX, int totalRow, int y, bool notFinalRow)
    {
        return x > initialPosX && x < totalRow - 1 && y > 0 && notFinalRow;
    }

    private bool IsDecoration()
    {
        return Random.Range(1, 6) == 1;
    }

    private bool IsGap()
    {
        return Random.Range(1, 20) == 1;
    }

    private void AddCoord(int x, int y, List<Coordinates> coords)
    {
        Coordinates newCoord = new Coordinates(x, y);
        coords.Add(newCoord);
    }
}
