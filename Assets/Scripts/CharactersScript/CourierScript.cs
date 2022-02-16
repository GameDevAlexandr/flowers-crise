using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CourierScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] targets;
    private GameManager gm;
    private int flowersType;
    [SerializeField] int flowersCount;
    public float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
