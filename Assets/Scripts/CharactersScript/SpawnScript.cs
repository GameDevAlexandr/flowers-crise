using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] shopers;
    [SerializeField] private ArrayScript spawnScheme;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private float timeBetweenShopers;
    [SerializeField] private Transform spawnPoint;
    private List<List<GameObject>> shopersWithWave;
    private List<GameObject> enemisForSpawn;
    private int waveNumber;
    private float waveTimer;
    private float shoperTimer;
    private bool startSpawn;
    private bool startWave;
    void Start()
    {
        for (int i = 0; i < shopers.Length; i++)
        {
            shopers[i].SetActive(false);
        }
        enemisForSpawn = new List<GameObject>();
        shopersWithWave = new List<List<GameObject>>();
        for(int i = 0; i < spawnScheme.wive.Length; i++)
        {
            for(int j=0; j < spawnScheme.wive[i].mobs.Length; j++)
            {
                for (int idx = 0; idx< spawnScheme.wive[i].mobs[j] + 1; idx++)
                {
                    shopersWithWave.Add (new List<GameObject>());
                    shopersWithWave[i].Add(GameObject.Instantiate(shopers[j]));
                }
            }
        }
        startSpawn = false;
        waveTimer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpawn)
        {   
            if(Time.time - shoperTimer>timeBetweenShopers)
                EnemySpawn();
        }
        else
        {
            if (Time.time - waveTimer > timeBetweenWaves)
                startSpawn = true;
        }
    }
    private void EnemySpawn()
    {
        if (shopersWithWave[waveNumber].Count != 0)
        {
            float spread = Random.Range(-3.0f, 3.0f);
            Vector3 position = spawnPoint.position;
            position.z += spread;
            shopersWithWave[waveNumber][0].transform.position = position;
            shopersWithWave[waveNumber][0].SetActive(true);
            shopersWithWave[waveNumber].RemoveAt(0);
            shoperTimer = Time.time;
        }
        else
        {
            if (waveNumber < shopersWithWave.Count - 1)
            {
                waveNumber++;
            }
            startSpawn = false;
            waveTimer = Time.time;
        }
    }
}
