using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private Transform[] pathElements;
    [SerializeField] private float timeBetweenWave;
    [SerializeField] private float timeBetweenEnemy;
    [SerializeField] private float spreadIndex;
    private List<GameObject> enemyObjects;
    private List<GameObject> enemyCounter;
    private GameManager gm;
    private float startWaveTimer;
    private float startSpawnTimer;
    private bool isStartWave;
    void Start()
    {
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
