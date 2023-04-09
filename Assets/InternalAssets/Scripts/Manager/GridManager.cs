using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int Rows;
    public int Columns;
    public GameObject[,] Grid;

    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private float cellSpacing;

    private void Awake()
    {
        InputManager.OnClick += SpawnHero;
    }
    private void Start()
    {
        Grid = new GameObject[Rows, Columns];

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                GameObject gridCell = Instantiate(gridCellPrefab, transform,true);
                float cellWidth = gridCell.GetComponent<Renderer>().bounds.size.x;
                float cellHeight = gridCell.GetComponent<Renderer>().bounds.size.y;
                float xPos = col * (cellWidth + cellSpacing);
                float yPos = row * (cellHeight + cellSpacing);
                gridCell.transform.position = new Vector3(xPos, 0, yPos);
                Grid[row, col] = gridCell;

                //GameObject gridCube = Instantiate(gridCubePrefab, transform);
                //gridCube.transform.position = gridCell.transform.position + new Vector3(0, 0.1f, 0);
            }
        }
    }
  
    public Vector3 GetCellPosition(int row, int col)
    {
        float cellWidth = Grid[0, 0].GetComponent<Renderer>().bounds.size.x;
        float cellHeight = Grid[0, 0].GetComponent<Renderer>().bounds.size.y;
        float xPos = col * (cellWidth + cellSpacing);
        float yPos = row * (cellHeight + cellSpacing);
        return new Vector3(xPos, 0, yPos);
    }

    public void SpawnHero()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                int row = -1;
                int col = -1;

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (Grid[i, j] == hitObject)
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
                   // GameObject cube = Instantiate(heroesPrefabs[InputManager.Instance.heroindex]);
                }
            }
    }
  
}
