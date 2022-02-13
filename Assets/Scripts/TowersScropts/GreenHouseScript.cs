using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenHouseScript : MonoBehaviour
{
    private TowerScript ts;
    [SerializeField] private int maxFlowers;
    [SerializeField] private int[] flowersType;
    [SerializeField] private int dropCountFlowers;
    private float timeBetweenCreate;
    private float lastCreateTime;
    private bool boostOn;
    private int typeCreated;
    private int[] boxCount;
    // Start is called before the first frame update
    void Start()
    {
        boxCount = new int[flowersType.Length];
        ts = GetComponent<TowerScript>();
        timeBetweenCreate = 60 / ts.speed;
        boostOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
