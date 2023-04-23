using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerVTwo : MonoBehaviour
{
    public int Rows;
    public int Columns;
    public float GridWidth;
    public float GridHeight;

    [SerializeField] private GameObject gridCellPrefab;


    public GameObject[,] Grid;

    private void Start()
    {
        Grid = new GameObject[Rows, Columns];

        float cellWidth = gridCellPrefab.GetComponent<Renderer>().bounds.size.x;
        float cellHeight = gridCellPrefab.GetComponent<Renderer>().bounds.size.y;

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                GameObject gridCell = Instantiate(gridCellPrefab, transform, true);

                float xPos = col * (cellWidth + (GridWidth - cellWidth * Columns) / (Columns - 1));
                float yPos = row * (cellHeight + (GridHeight - cellHeight * Rows) / (Rows - 1));

                gridCell.transform.position = new Vector3(xPos, 0, yPos);
                Grid[row, col] = gridCell;

                //GameObject gridCube = Instantiate(gridCubePrefab, transform);
                //gridCube.transform.position = gridCell.transform.position + new Vector3(0, 0.1f, 0);
            }
        }
       // this.transform.localScale = Vector3.one * 10f;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        GameObject hitObject = hit.collider.gameObject;
        //        int row = -1;
        //        int col = -1;

        //        for (int i = 0; i < Rows; i++)
        //        {
        //            for (int j = 0; j < Columns; j++)
        //            {
        //                if (Grid[i, j] == hitObject)
        //                {
        //                    row = i;
        //                    col = j;
        //                    break;
        //                }
        //            }

        //            if (row >= 0 && col >= 0)
        //            {
        //                break;
        //            }
        //        }

        //        if (row >= 0 && col >= 0)
        //        {
        //            Vector3 cellPos = GetCellPosition(row, col);
        //            GameObject cube = Instantiate(gridCubePrefab);
        //            cube.transform.position = cellPos + new Vector3(0, 0, 0);
        //        }
        //    }
        //}
    }

    public Vector3 GetCellPosition(int row, int col)
    {
        float cellWidth = Grid[0, 0].GetComponent<Renderer>().bounds.size.x;
        float cellHeight = Grid[0, 0].GetComponent<Renderer>().bounds.size.y;

        float xPos = col * (cellWidth + (GridWidth - cellWidth * Columns) / (Columns - 1));
        float yPos = row * (cellHeight + (GridHeight - cellHeight * Rows) / (Rows - 1));

        return new Vector3(xPos, 0, yPos);
    }
}