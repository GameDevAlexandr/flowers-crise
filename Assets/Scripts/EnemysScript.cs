using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemysScript : MonoBehaviour
{
    private int[] maxFlowersType;
    private NavMeshAgent agent;
    private GameManager gm;
    private GameObject[] target;
    [SerializeField] private int priceOfSatisfy;
    [SerializeField] private float speed;
    [SerializeField] private Image[] UIflowers;
    public int[] flowersTypeNeed;
    public bool isSatisfy;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        target = GameObject.FindGameObjectsWithTag("EnemyFinish");
        int targetIndex = Random.Range(0, target.Length);
        agent.destination = target[targetIndex].transform.position;
        isSatisfy = false;
        maxFlowersType = new int[flowersTypeNeed.Length];
        for (int i = 0; i < flowersTypeNeed.Length; i++)
        {
            maxFlowersType[i] = flowersTypeNeed[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
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
