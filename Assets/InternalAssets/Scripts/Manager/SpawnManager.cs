using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> Heroes = default;
       // Start is called before the first frame update
    public List<SpawnData> spawnData = new List<SpawnData>();
    int heroIndex=0;
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

                for (int i = 0; i < GameplayManager.Instance.GridManager_Two.Rows; i++)
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
                    
                    if (GameplayManager.Instance.GridManager_Two.Grid[row, col].GetComponent<Tile>().IsOccupied || 
                        !GameplayManager.Instance.CoinsManager.HasEnoughCoins(Heroes[heroIndex].GetComponent<StatSystem>().characterData.TierI_Cost))
                    {
                        return;
                    }
                    Vector3 cellPos = GameplayManager.Instance.GridManager_Two.GetCellPosition(row, col);
                    GameObject _hero = Instantiate(Heroes[heroIndex]);
                    _hero.transform.position = cellPos + new Vector3(0, 0, 1f);
                    GameplayManager.Instance.GridManager_Two.Grid[row, col].GetComponent<Tile>().IsOccupied = true;
                    GameplayManager.Instance.CoinsManager.AddCoins(-Heroes[heroIndex].GetComponent<StatSystem>().characterData.TierI_Cost);
                    Heroes[heroIndex].GetComponent<StatSystem>().rowPosition = row;
                    Heroes[heroIndex].GetComponent<StatSystem>().colPosition = col;

                }
            }
        }
    }

    public void SelectHero(int index)
    {
        heroIndex = index;
    }
}

[System.Serializable]

public class SpawnData
{
    public bool isPlaced;
    public int[,] location;
}
