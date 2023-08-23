using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public static event Action<int> OnWaveCountUpdated;
    [SerializeField] private float spawnInterval = 5f;
    public List<CharacterData> Enemies;
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocation;
    public int spawnIndex;

    public int waveDuration;
    private float waveTimer;
    private float spawnTimer;

    public int level;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public List<GameObject> EnemiesGameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                int spawnlocation = UnityEngine.Random.Range(0, 5);
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnlocation].position, spawnLocation[spawnlocation].rotation); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
                EnemiesGameObject.Add(enemy);

                if (spawnIndex + 1 <= spawnLocation.Length - 1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
        }

        if (waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currWave++;
            GenerateWave();
            OnWaveCountUpdated?.Invoke(currWave);
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * level + UnityEngine.Random.Range(1, 11);
        spawnTimer = Mathf.Clamp((waveDuration / 3f), 25f, 40f);
        waveDuration += 60;
        waveTimer = waveDuration; // wave duration is read only
        GenerateEnemies();
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 20)
        {
            int randEnemyId = UnityEngine.Random.Range(0, Enemies.Count);
            int randEnemyCost = Enemies[randEnemyId].Cost;
            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(Enemies[randEnemyId].CharacterPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    public void KilledInAction(GameObject gameObject)
    {
        if (spawnedEnemies.Contains(gameObject))
        {
            spawnedEnemies.Remove(gameObject);
        }
    }

    public void ResetWaveSystem()
    {
        foreach (GameObject item in EnemiesGameObject)
        {
            Destroy(item);
        }

        EnemiesGameObject.Clear();
        currWave = 1;
        waveDuration = 15;
        waveTimer = 0;
    }

}

