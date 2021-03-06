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
    private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = animator.speed / 4 * speed;
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
                    targetMarket.used = false;
                    flowerImage.gameObject.SetActive(false);
                }
                else
                {
                    agent.destination = gh.flowerMarket.transform.position;
                    flowerType = gh.needFlower;
                    flowerImage.gameObject.SetActive(true);
                    flowerImage.sprite = flowersIco[flowerType];
                    targetMarket = gh.flowerMarket;
                    gh.flowerMarket.used = true;
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
    public void Boost(bool isBoost)
    {
        if (isBoost)
        {
            agent.speed *= 2; 
            animator.speed *= 2;

        }
        else
        {
            agent.speed *= 0.5f;
            animator.speed *= 0.5f;
        }
    }
}
