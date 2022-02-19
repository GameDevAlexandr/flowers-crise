using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemysScript : MonoBehaviour
{
    public int[] flowersTypeNeed;
    [HideInInspector]public bool isSatisfy;
    public List<Vector3> pathElemeents;
    [SerializeField] private int priceOfSatisfy;
    [SerializeField] private float speed;
    [SerializeField] private Image[] UIflowers;
    private int[] maxFlowersType;
    private int pathIndex;
    private NavMeshAgent agent;
    private GameManager gm;
    private bool isStarted;
        
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        isSatisfy = false;
        maxFlowersType = new int[flowersTypeNeed.Length];
        for (int i = 0; i < flowersTypeNeed.Length; i++)
        {
            maxFlowersType[i] = flowersTypeNeed[i];
        }
        pathIndex = 1;
        isStarted = false;
    }
    private void Start()
    {
 
    }
    public void startEnemy()
    {
        agent.destination = pathElemeents[pathIndex];        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            startEnemy();
            isStarted = true;
        }
        if (agent.remainingDistance == 0 && pathIndex < pathElemeents.Count)
        {
            pathIndex++;
            startEnemy();
        }
    }
    public void AddFlowers(int flowersCount, int index)
    {
        flowersTypeNeed[index] -= flowersCount;
        if (flowersTypeNeed[index] <= 0)
        {
            flowersTypeNeed[index] = 0;
        }
        float fillIdx = 1 / (float)maxFlowersType[index];
        UIflowers[index].fillAmount = fillIdx * flowersTypeNeed[index];
        if (index == flowersTypeNeed.Length-1 && flowersTypeNeed[index] == 0)
        {
            isSatisfy = true;
            gm.AddMoney(priceOfSatisfy);
        }
    }
    public void Freeze()
    {
        agent.speed = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyFinish")
        {
            if (!isSatisfy)
            {
                gm.FlipPage(1);
            }
            Destroy(gameObject);
        }
    }
}
