using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapProcessor : MonoBehaviour
{
    public enum CellType { Empty, Filled, Ground }

    public CellType[,] Grid { get; private set; }

    private Tilemap tilemap;
    private TilemapCollider2D tilemapCollider2D;

    private const int cellsAboveCollider = 8;

    private void Awake()
    {
        /* Attempt to find tilemap object in scene */
        GameObject tilemapGameObject;
        try
        {
            tilemapGameObject = GameObject.Find("Grid").transform.Find("Default").gameObject;
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Tilemap not found! " + e.StackTrace);
            return;
        }

        /* Initialize private fields */
        tilemap = tilemapGameObject.GetComponent<Tilemap>();
        tilemapCollider2D = tilemapGameObject.GetComponent<TilemapCollider2D>();

        /* Initialize Grid property */
        Grid = new CellType[Mathf.CeilToInt(2 * tilemapCollider2D.bounds.extents.x),
                            Mathf.CeilToInt(2 * tilemapCollider2D.bounds.extents.y) + cellsAboveCollider];

        /* Fill grid with data from tilemap */
        FillGrid();
    }

    private void FillGrid()
    {
        /* Iterate through each cell in grid */
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                Vector3Int pos = new Vector3Int(GridToTilemapX(x), GridToTilemapY(y), 0);
                Vector3Int below = new Vector3Int(pos.x, pos.y - 1, 0);

                /* Check for filled tile */
                if (tilemap.GetColliderType(pos) != Tile.ColliderType.None)
                {
                    Grid[x, y] = CellType.Filled;
                }
                /* Check for ground tile */
                else if (y > 0 && tilemap.GetColliderType(below) != Tile.ColliderType.None)
                {
                    Grid[x, y] = CellType.Ground;
                }
                /* Otherwise, empty tile */
                else
                {
                    Grid[x, y] = CellType.Empty;
                }
            }
        }
    }

    private int GridToTilemapX(int x)
    {
        return (int)(x + tilemapCollider2D.bounds.center.x - tilemapCollider2D.bounds.extents.x);
    }

    private int GridToTilemapY(int y)
    {
        return (int)(y + tilemapCollider2D.bounds.center.y - tilemapCollider2D.bounds.extents.y);
    }

    private int TilemapToGridX(int x)
    {
        return (int)(x - tilemapCollider2D.bounds.center.x + tilemapCollider2D.bounds.extents.x);
    }

    private int TilemapToGridY(int y)
    {
        return (int)(y - tilemapCollider2D.bounds.center.y + tilemapCollider2D.bounds.extents.y);
    }
}
