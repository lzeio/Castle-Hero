using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerVTwo : MonoBehaviour
{
    public int rows;
    public int columns;

    [SerializeField] private GameObject gridCubePrefab;
    [SerializeField]private GameObject gridCellPrefab;

    public float gridWidth;
    public float gridHeight;

    private GameObject[,] grid;

    private void Start()
    {
        grid = new GameObject[rows, columns];

        float cellWidth = gridCellPrefab.GetComponent<Renderer>().bounds.size.x;
        float cellHeight = gridCellPrefab.GetComponent<Renderer>().bounds.size.y;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject gridCell = Instantiate(gridCellPrefab, transform, true);

                float xPos = col * (cellWidth + (gridWidth - cellWidth * columns) / (columns - 1));
                float yPos = row * (cellHeight + (gridHeight - cellHeight * rows) / (rows - 1));

                gridCell.transform.position = new Vector3(xPos, 0, yPos);
                grid[row, col] = gridCell;

                //GameObject gridCube = Instantiate(gridCubePrefab, transform);
                //gridCube.transform.position = gridCell.transform.position + new Vector3(0, 0.1f, 0);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                int row = -1;
                int col = -1;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (grid[i, j] == hitObject)
                        {
                            row = i;
                            col = j;
                            break;
                        }
                    }

                    if (row >= 0 && col >= 0)
                    {
                        break;
                    }
                }

                if (row >= 0 && col >= 0)
                {
                    Vector3 cellPos = GetCellPosition(row, col);
                    GameObject cube = Instantiate(gridCubePrefab);
                    cube.transform.position = cellPos + new Vector3(0, 0, 0);
                }
            }
        }
    }

    public Vector3 GetCellPosition(int row, int col)
    {
        float cellWidth = grid[0, 0].GetComponent<Renderer>().bounds.size.x;
        float cellHeight = grid[0, 0].GetComponent<Renderer>().bounds.size.y;

        float xPos = col * (cellWidth + (gridWidth - cellWidth * columns) / (columns - 1));
        float yPos = row * (cellHeight + (gridHeight - cellHeight * rows) / (rows - 1));

        return new Vector3(xPos, 0, yPos);
    }
}