using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour
{
    
    
    public float speed;
    [HideInInspector] public List<FlowersMarketScript> destination;
    private int flowerType;
    private Vector3 startPosition;
    [SerializeField] int flowersCount;
    void Start()
    {
        startPosition = transform.position;
        destination = new List<FlowersMarketScript>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < destination.Count; i++)
        {
            if(other.gameObject == destination[i].gameObject)
            {
                destination[i].GetFlowers(flowerType, flowersCount);
                flowersCount = 0;
            }
        }
    }
}
