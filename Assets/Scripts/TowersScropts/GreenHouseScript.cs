using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenHouseScript : MonoBehaviour
{
    [HideInInspector] public Vector3 neededPosition;
    [SerializeField] private int workersCount;
    [SerializeField] private GameObject worker;
    private TowerScript ts;
    private GameObject[] workers;
    private WorkerScript[] ws;
    private List<FlowersMarketScript> markets;
    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        markets = new List<FlowersMarketScript>();
        workers = new GameObject[workersCount];
        for (int i = 0; i < workersCount; i++)
        {
            workers[i] = GameObject.Instantiate(worker, transform.position, Quaternion.identity);
            ws[i] = workers[i].GetComponent<WorkerScript>();
        }
        ts = GetComponent<TowerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        int needItem = 10000;
        FlowersMarketScript fms = new FlowersMarketScript();
        if (markets.Count != 0)
        {
            for (int i = 0; i < markets.Count; i++)
            {
                int item = markets[i].flowersType[markets[i].needFlowerType];
                if (item < needItem)
                {
                    needItem = item;
                    fms = markets[i];
                }
            }
            neededPosition = fms.transform.position;
        }
    }
    private void GetTowers()
    {
        markets.Clear();
        for (int i = 0; i <gm.marketInScene.Count ; i++)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, gm.marketInScene[i].transform.position)) <= ts.radius)
            {
                markets.Add(gm.marketInScene[i]);
            } 
        }
    }
}
