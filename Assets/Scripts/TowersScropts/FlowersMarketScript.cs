using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersMarketScript : MonoBehaviour
{  
    public int[] flowersType;
    public bool itsWineMarket;
    [HideInInspector] public int needFlowerType;
    [HideInInspector] public bool used;
    [SerializeField] private int[] maxFlowers;
    [SerializeField] private int dropCountFlowers;
    [SerializeField] GameObject shotElement;
    [SerializeField] Transform[] firePosition;
    [SerializeField]private float wineStrenght;
    [SerializeField]private float wineActionTime;
    private TowerScript ts;
    private FlowerScript bullet;
    private float timeBetweenShot;
    private float lastShotTime;
    private bool boostOn;
    private TowerUIScript tUI;
    // Start is called before the first frame update
    void Start()
    {
        tUI = GetComponent<TowerUIScript>();
        shotElement = GameObject.Instantiate(shotElement, transform.position, Quaternion.identity);
        bullet = shotElement.GetComponent<FlowerScript>();
        ts = GetComponent<TowerScript>();
        timeBetweenShot = 60 / ts.speed;
        boostOn = false;
        for (int i = 0; i < tUI.flowersCounters.Length; i++)
        {
            tUI.flowersCounters[i].fillAmount =  1 / (float)maxFlowers[i] * flowersType[i];
        }
        if (itsWineMarket)
        {
            needFlowerType = 3;
        }
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
        if (flowersType[index] + count > maxFlowers[index])
        {
            count = maxFlowers[index] - flowersType[index];
        }
        flowersType[index] += count;
        tUI.flowersCounters[index].fillAmount = 1 / (float)maxFlowers[index] * flowersType[index];
    }
    private void DropFlowers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ts.radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            
            EnemysScript es = hitColliders[i].GetComponent<EnemysScript>();
            if (hitColliders[i].tag == "Enemy" && !es.isSatisfy)
            {
                if (!itsWineMarket)
                {
                    for (int j = 0; j < es.flowersTypeNeed.Length; j++)
                    {
                        if (flowersType[j] >= dropCountFlowers && es.flowersTypeNeed[j] > 0)
                        {
                            bullet.Shot(firePosition[j].position, es.transform.position, j);
                            es.AddFlowers(dropCountFlowers, j);
                            GetFlowers(j, -dropCountFlowers);
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    if (flowersType[3] >= dropCountFlowers && !es.isDrunk)
                    {
                        GetFlowers(3, -dropCountFlowers);
                        es.AddWine(wineStrenght,wineActionTime);
                        bullet.Shot(transform.position, es.transform.position, 3);
                        break;
                    }                    
                }
            }
        }
    }
    public void SetNeededFlower(int flowerNeed)
    {
        needFlowerType = flowerNeed;
    }
    public void Boosting(bool active)
    {

    }
}
