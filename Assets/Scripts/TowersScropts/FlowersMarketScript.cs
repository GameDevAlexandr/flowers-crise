using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersMarketScript : MonoBehaviour
{
    private TowerScript ts;
    [SerializeField] private int maxFlowers;
    [SerializeField] private int[] flowersType;
    [SerializeField] private int dropCountFlowers;
    private float timeBetweenShot;
    private float lastShotTime;
    private bool boostOn;
    // Start is called before the first frame update
    void Start()
    {
        ts = GetComponent<TowerScript>();
        timeBetweenShot = 60 / ts.speed;
        boostOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastShotTime > timeBetweenShot)
        {
            DropFlowers();
            lastShotTime = Time.time;
        }
        if (!boostOn && ts.boostOn)
        {
            boostOn = true;
            Boosting(true);
        }
        if (!ts.boostOn)
        {
            boostOn = false;
            Boosting(false);
        }
    }
    public void GetFlowers(int index, int count)
    {
        int flowersCounter = 0;
        for (int i = 0; i < flowersType.Length; i++)
        {
            flowersCounter += flowersType[i];
        }
        if (count > maxFlowers - flowersCounter)
        {
            count = maxFlowers - flowersCounter;
        }
        flowersType[index] += count;
    }
    private void DropFlowers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ts.radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            EnemysScript es = hitColliders[i].GetComponent<EnemysScript>();
            if (hitColliders[i].tag == "Enemy" && !es.isSatisfy)
            {
                for (int j = 0; j < es.flowersTypeNeed.Length; j++)
                {
                    if (flowersType[j] >= dropCountFlowers && es.flowersTypeNeed[j] > 0)
                    {
                        es.AddFlowers(dropCountFlowers, j);
                        flowersType[j] -= dropCountFlowers;
                        break;
                    }
                }
            }
        }
    }
    public void Boosting(bool active)
    {

    }
}
