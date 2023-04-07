using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;

    public GameObject gridCellPrefab;
    [SerializeField] private List<GameObject> heroesPrefabs = default;

    public float cellSpacing;

    private GameObject[,] grid;
    private void Awake()
    {
        InputManager.OnClick += SpawnHero;
    }
    private void Start()
    {
        grid = new GameObject[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject gridCell = Instantiate(gridCellPrefab, transform,true);
                float cellWidth = gridCell.GetComponent<Renderer>().bounds.size.x;
                float cellHeight = gridCell.GetComponent<Renderer>().bounds.size.y;
                float xPos = col * (cellWidth + cellSpacing);
                float yPos = row * (cellHeight + cellSpacing);
                gridCell.transform.position = new Vector3(xPos, 0, yPos);
                grid[row, col] = gridCell;

                //GameObject gridCube = Instantiate(gridCubePrefab, transform);
                //gridCube.transform.position = gridCell.transform.position + new Vector3(0, 0.1f, 0);
            }
        }
    }
  
    public Vector3 GetCellPosition(int row, int col)
    {
        float cellWidth = grid[0, 0].GetComponent<Renderer>().bounds.size.x;
        float cellHeight = grid[0, 0].GetComponent<Renderer>().bounds.size.y;
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
                    GameObject cube = Instantiate(heroesPrefabs[InputManager.Instance.heroindex]);
                    cube.transform.position = cellPos + new Vector3(0, 0, 0);
                }
            }
    }
  
}
