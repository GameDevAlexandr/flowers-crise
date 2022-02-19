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
    [SerializeField] private Transform[] pathElements;
    [SerializeField] private float spreadIndex;
    private List<List<GameObject>> shopersWithWave;
    private List<GameObject> enemisForSpawn;
    private int waveNumber;
    private float waveTimer;
    private float shoperTimer;
    private bool startSpawn;
    private bool startWave;
    void Start()
    {
        enemisForSpawn = new List<GameObject>();
        shopersWithWave = new List<List<GameObject>>();
        for (int i = 0; i < spawnScheme.wive.Length; i++)
        {
            for (int j = 0; j < spawnScheme.wive[i].mobs.Length; j++)
            {
                for (int idx = 0; idx < spawnScheme.wive[i].mobs[j] + 1; idx++)
                {
                    shopersWithWave.Add(new List<GameObject>());
                    GameObject newObject = GameObject.Instantiate(shopers[j]);
                    shopersWithWave[i].Add(newObject);
                    float spreadX = Random.Range(-spreadIndex, spreadIndex);
                    float spreadY = Random.Range(-spreadIndex, spreadIndex);
                    EnemysScript es = newObject.GetComponent<EnemysScript>();
                    for (int k = 0; k < pathElements.Length; k++)
                    {
                        Vector3 positions = pathElements[k].position;
                        positions.x += spreadX;
                        positions.z += spreadY;
                        es.pathElemeents.Add(positions);
                    }
                    newObject.transform.position = es.pathElemeents[0];
                    newObject.SetActive(false);
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
