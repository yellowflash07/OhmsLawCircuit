using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private Vector2Int gridSize;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var instCell = Instantiate(cell, transform);
                instCell.transform.localPosition = new Vector3(i, 0, j);
            }
        }
    }
}
