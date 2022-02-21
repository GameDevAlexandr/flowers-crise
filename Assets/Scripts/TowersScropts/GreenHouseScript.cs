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

    void Start()
    {
        firstWorkerIsCreate = false;
        markets = new List<FlowersMarketScript>();
        ts = GetComponent<TowerScript>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.updateTowers();
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
                GameObject newWorker = GameObject.Instantiate(worker, transform.position, Quaternion.identity);
                WorkerScript ws = newWorker.GetComponent<WorkerScript>();
                ws.StartWorker(gameObject);
                firstWorkerIsCreate = true;
            }
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
}
