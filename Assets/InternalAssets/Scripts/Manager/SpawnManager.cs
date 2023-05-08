using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static event Action<bool> OnHeroSpawn;
    public int heroIndex { get; private set; }
    public bool hasChampion { get; private set; }
    [SerializeField] private Button championButton;
    public List<CharacterData> Heroes = default;

    private void Start()
    {
        InputManager.TrySpawnHero += SpawnHero;
        IamChampion.OnChampionIsSlain += ChampionIsSlained;
        heroIndex = 80;
    }

    private void SpawnHero()
    {
        if (heroIndex >= Heroes.Count) { return; }
        if (heroIndex == 5 && hasChampion) { return; }
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
                    !GameplayManager.Instance.CoinsManager.HasEnoughCoins(Heroes[heroIndex].Cost))
                {
                    return;
                }
                InstantiateHero(row, col);
                GameplayManager.Instance.CoinsManager.AddCoins(-Heroes[heroIndex].Cost);

            }
        }
    }

 

    public void InstantiateHero(int row, int col)
    {
        Vector3 cellPos = GameplayManager.Instance.GridManager_Two.GetCellPosition(row, col);
        GameObject _hero = Instantiate(Heroes[heroIndex].CharacterPrefab);
        _hero.transform.position = cellPos + new Vector3(0, 0, 1f);
        SetPosition(_hero, row, col);
        if (heroIndex == 5)
        {
            hasChampion = true;
            championButton.interactable = false;
            OnHeroSpawn?.Invoke(true);
            return;
        }
        OnHeroSpawn?.Invoke(false);
    }

    public void SetPosition(GameObject hero, int row, int col)
    {
        GameplayManager.Instance.GridManager_Two.Grid[row, col].GetComponent<Tile>().IsOccupied = true;
        hero.GetComponent<StatSystem>().rowPosition = row;
        hero.GetComponent<StatSystem>().colPosition = col;
    }

    public void SelectHero(int index)
    {
        if (hasChampion && index == 5) return;
        heroIndex = index;
    }

    public void ChampionIsSlained()
    {
        hasChampion = false;
    }
    public void DisableSpawning()
    {
        heroIndex = 80;
    }
}
