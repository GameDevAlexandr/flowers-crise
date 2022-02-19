using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WorkerScript : MonoBehaviour
{
    
    
    public float speed;
    private int flowerType;
    private Vector3 startPosition;
    private GreenHouseScript gh;
    [SerializeField] int flowersCount;
    [SerializeField] Sprite[] flowersIco;
    [SerializeField] Image flowerImage;
    private NavMeshAgent agent;
    private bool toMarket;
    private FlowersMarketScript targetMarket;
    private bool isStarted;
    void Start()
    {

    }
    void Update()
    {
        if (isStarted)
        {
            if (agent.remainingDistance == 0)
            {
                if (toMarket)
                {
                    agent.destination = startPosition;
                    toMarket = false;
                    targetMarket.GetFlowers(flowerType, flowersCount);
                    flowerImage.gameObject.SetActive(false);
                }
                else
                {
                    agent.destination = gh.flowerMarket.transform.position;
                    flowerType = gh.needFlower;
                    flowerImage.gameObject.SetActive(true);
                    flowerImage.sprite = flowersIco[flowerType];
                    targetMarket = gh.flowerMarket;
                    toMarket = true;
                }

            }
        }
    }
    public void StartWorker(GameObject meTower)
    {
        gh = meTower.GetComponent<GreenHouseScript>();
        flowerType = gh.needFlower;
        flowerImage.gameObject.SetActive(true);
        flowerImage.sprite = flowersIco[flowerType];
        toMarket = true;
        startPosition = transform.position;
        targetMarket = gh.flowerMarket;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.destination = gh.flowerMarket.transform.position;
        isStarted = true;
    }
}
