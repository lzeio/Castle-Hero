using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> Heroes = default;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

                for (int i = 0; i <GameplayManager.Instance.GridManager_Two.Rows; i++)
                {
                    for (int j = 0; j < GameplayManager.Instance.GridManager_Two.Columns; j++)
                    {
                        if (GameplayManager.Instance.GridManager_Two.Grid[i, j] == hitObject)
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
                    Vector3 cellPos = GameplayManager.Instance.GridManager_Two.GetCellPosition(row, col);
                    GameObject cube = Instantiate(Heroes[GameplayManager.Instance.InputManager.heroindex]);
                    cube.transform.position = cellPos + new Vector3(0, 0, 0);
                }
            }
        }
    }
}
