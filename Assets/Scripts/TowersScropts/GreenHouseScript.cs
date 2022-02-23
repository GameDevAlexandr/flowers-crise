using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenHouseScript : MonoBehaviour
{
    public Vector3 neededPosition;
    public int needFlower;
    [SerializeField] private int workersCount;
    [SerializeField] private GameObject worker;
    private TowerScript ts;
    private bool firstWorkerIsCreate;
    private List<FlowersMarketScript> markets;
    public FlowersMarketScript flowerMarket;
    private GameManager gm;
    private List<WorkerScript> workers;
    private bool boosting;

    void Start()
    {
        workers = new List<WorkerScript>();
        firstWorkerIsCreate = false;
        markets = new List<FlowersMarketScript>();
        ts = GetComponent<TowerScript>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.updateTowers();
        boosting = false;
    }

    // Update is called once per frame
    void Update()
    {
        int needItem = 10000;
        if (markets.Count != 0)
        {
            for (int i = 0; i < markets.Count; i++)
            {
                int item = markets[i].flowersType[markets[i].needFlowerType];
                if (item < needItem && !markets[i].used)
                {
                    needItem = item;
                    needFlower = markets[i].needFlowerType;
                    neededPosition = markets[i].transform.position;
                    flowerMarket = markets[i];
                }
            }
            if (!firstWorkerIsCreate)
            {
                firstWorkerIsCreate = true;
            }
        }
        if (ts.isUpgrade||workers.Count==0&&markets.Count!=0)
        {
            ts.isUpgrade = false;
            WorkerCreate();
        }
        if (ts.boostOn && !boosting)
        {
            for (int i = 0; i < workers.Count; i++)
            {
                workers[i].Boost(true);
            }
            boosting = true;
        }
        else if(!ts.boostOn&&boosting)
        {
            for (int i = 0; i < workers.Count; i++)
            {
                workers[i].Boost(false);
            }
            boosting = false;
        }
    }
    public void GetTowers()
    {
        if (markets.Count != 0)
        {
            markets.Clear();
        }
        for (int i = 0; i <gm.marketInScene.Count ; i++)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, gm.marketInScene[i].transform.position)) <= ts.radius)
            {
                markets.Add(gm.marketInScene[i]);
            } 
        }
    }
    private void WorkerCreate()
    {
        GameObject newWorker = GameObject.Instantiate(worker, transform.position, Quaternion.identity);
        newWorker.transform.parent = transform;
        WorkerScript ws = newWorker.GetComponent<WorkerScript>();
        ws.StartWorker(gameObject);
        workers.Add(ws);
    }
}
