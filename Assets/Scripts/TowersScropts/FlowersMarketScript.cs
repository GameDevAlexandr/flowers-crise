using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersMarketScript : MonoBehaviour
{
    public GameObject bulletObject;
    [SerializeField] private float bulletSpeed;
    public int SellerType;
    public float speed;
    public float radius;
    public int dropCountFlowers;
    public int priceUpgrade;
    private TowerScript ts;
    private float timeBetweenShot;
    private float lastShotTime;
    [HideInInspector] public BulletScript bullet;
    void Start()
    {
        ts = GetComponentInParent<TowerScript>();
        timeBetweenShot = 60 / speed;
        bulletObject = GameObject.Instantiate(bulletObject,transform.position,Quaternion.identity);
        bullet = bulletObject.GetComponent<BulletScript>();
        bullet.speed = bulletSpeed;
        bulletObject.SetActive(false);
    }


    void Update()
    {
        if(Time.time - lastShotTime > timeBetweenShot)
        {
            DropFlowers();
            lastShotTime = Time.time;
        }
    }

    private void DropFlowers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            EnemysScript es = hitColliders[i].GetComponent<EnemysScript>();
            if (hitColliders[i].tag == "Enemy" && !es.isSatisfy)
            {
                for (int j = 0; j < es.flowersTypeNeed.Length; j++)
                {
                    if (ts.bulletTypeCount[j] >= dropCountFlowers && es.flowersTypeNeed[j] > 0)
                    {
                        if (SellerType == j)
                        {
                            bullet.shot(hitColliders[i].transform.position, j);
                            bullet.gameObject.SetActive(true);
                            es.AddFlowers(dropCountFlowers, j);
                            ts.AddFlowers(j, -dropCountFlowers);
                            break;
                        }
                    }                    
                }
                break;
            }
        }
    }
    public void Upgrade()
    {

    }
}
