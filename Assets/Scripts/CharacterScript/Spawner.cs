using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private Transform[] pathElements_1;
    [SerializeField] private Transform[] pathElements_2;
    [SerializeField] private Transform[] pathElements_3;
    [SerializeField] private float timeBetweenWave;
    [SerializeField] private float timeBetweenEnemy;
    [SerializeField] private float spreadIndex;
    private List<GameObject> enemyObjects;
    private List<GameObject> enemyCounter;
    private GameManager gm;
    private float startWaveTimer;
    private float startSpawnTimer;
    private bool isStartWave;
    private Transform[][] pathElements;
    void Start()
    {
        pathElements = new Transform[3][];
        pathElements[0] = new Transform[pathElements_1.Length];
        pathElements[1] = new Transform[pathElements_2.Length];
        pathElements[2] = new Transform[pathElements_3.Length];
        pathElements[0] = pathElements_1;
        pathElements[1] = pathElements_2;
        pathElements[2] = pathElements_3;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyObjects = new List<GameObject>();
        enemyCounter = new List<GameObject>();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            if (enemys[i].GetComponent<EnemysScript>() != null)
            {
                GameObject newObject = GameObject.Instantiate(enemys[i]);
                Debug.Log(newObject.name);
                enemyObjects.Add(newObject);
                int rndIndex =0;
                for (int j = 0; j < pathElements.Length; j++)
                {
                    if (pathElements[j].Length!=0)
                    {
                        rndIndex++;
                    }
                }
                int choosePath = Random.Range(0, rndIndex);
                float spreadX = Random.Range(-spreadIndex, spreadIndex);
                float spreadY = Random.Range(-spreadIndex, spreadIndex);
                EnemysScript es = newObject.GetComponent<EnemysScript>();
                for (int k = 0; k < pathElements[choosePath].Length; k++)
                {
                    Vector3 positions = pathElements[choosePath][k].position;
                    positions.x += spreadX;
                    positions.z += spreadY;
                    es.pathElemeents.Add(positions);
                }
                newObject.transform.position = es.pathElemeents[0];
                enemyCounter.Add(newObject);
            }
            else
            {
                enemyObjects.Add(null);
            }
        }
        gm.enemys = enemyCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startSpawnTimer > timeBetweenEnemy&&!isStartWave&&enemyObjects.Count!=0)
        {
            Spawn();
        }
        if(isStartWave && Time.time - startWaveTimer > timeBetweenWave)
        {
            isStartWave = false;
        }
    }
    private void Spawn()
    {
        if (enemyObjects[0] != null)
        {
            enemyObjects[0].SetActive(true);
            enemyObjects.Remove(enemyObjects[0]);
            startSpawnTimer = Time.time;
        }
        else
        {
            enemyObjects.Remove(enemyObjects[0]);
            isStartWave = true;
            startWaveTimer = Time.time;
        }
    }
}
