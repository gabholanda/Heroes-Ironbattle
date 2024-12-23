﻿using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Map Items", menuName = "ScriptableObjects/Map/Items")]
public class MapItems : ScriptableObject
{
    public Tile[] outerBottomTiles;
    public Tile[] outerTopTiles;
    public Tile[] bottomTiles;
    public Tile[] middleTiles;
    public Tile[] topTiles;
    public MapType type;
}
