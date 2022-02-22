using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerScript : MonoBehaviour
{
    [SerializeField] private bool itsWineMarket;
    [SerializeField] private int dropCountFlowers;
    [HideInInspector] public TowerScript ts;
    [HideInInspector] public FlowersMarketScript fms;
    [SerializeField] private GameObject bulletObject;
    private float timeBetweenShot;
    private float lastShotTime;
    private FlowerScript bullet;

    void Start()
    {
        bulletObject = GameObject.Instantiate(bulletObject, transform.position, Quaternion.identity);
        bullet = bulletObject.GetComponent<FlowerScript>();
        ts = GetComponentInParent<TowerScript>();
        fms = GetComponentInParent<FlowersMarketScript>();
        timeBetweenShot = 60 / ts.speed;
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
                        if (fms.flowersType[j] >= dropCountFlowers && es.flowersTypeNeed[j] > 0)
                        {
                            bullet.Shot(transform.position, es.transform.position, j);
                            es.AddFlowers(dropCountFlowers, j);
                            fms.GetFlowers(j, -dropCountFlowers);
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    if (fms.flowersType[3] >= dropCountFlowers && !es.isDrunk)
                    {
                        fms.GetFlowers(3, -dropCountFlowers);
                        es.AddWine(fms.wineStrenght, fms.wineActionTime);
                        bullet.Shot(transform.position, es.transform.position, 3);
                        break;
                    }
                }
            }
        }
    }
    void Update()
    {
        if (Time.time - lastShotTime > timeBetweenShot)
        {
            DropFlowers();
            lastShotTime = Time.time;
        }
    }
}
