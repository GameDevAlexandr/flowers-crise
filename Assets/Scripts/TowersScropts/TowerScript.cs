using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    public ParticleSystem radiusSphere;
    public int[] bulletTypeCount;
    public int[] maxBulletTypeCount;
    [SerializeField] private GameObject[] sellersObject;
    [SerializeField] private int maxFlowers;
    [SerializeField] private int[] sellerUpgradePrice;
    private GameManager gm;
    private UIItem ui;
    private FlowersMarketScript[] sellers;
    void Start()
    {
        sellers = new FlowersMarketScript[sellersObject.Length];
        ui = GetComponent<UIItem>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < sellers.Length; i++)
        {
            sellers[i] = sellersObject[i].GetComponent<FlowersMarketScript>();
            ui.upgradeSellerText[i].text = sellers[i].priceUpgrade.ToString();
        }
        for (int i = 0; i < maxBulletTypeCount.Length; i++)
        {
            ui.flowrCounters[i].fillAmount = 1 / (float)maxBulletTypeCount[i] * bulletTypeCount[i];
        }
        setRadius();
    }

    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (ui.upgradeSellers[i].gameObject.activeSelf)
            {
                if (int.Parse(ui.upgradeSellerText[i].text) <= gm.moneyCount)
                {
                    ui.upgradeSellers[i].interactable = true;
                }
                else
                {
                    ui.upgradeSellers[i].interactable = false;
                }
            }
            if (i<2&& ui.addSellers[i].gameObject.activeSelf)
            {
                if (int.Parse(ui.addSellerText[i].text) <= gm.moneyCount)
                {
                    ui.addSellers[i].interactable = true;
                }
                else
                {
                    ui.addSellers[i].interactable = false;
                }
            }
        }
    }
    public void AddSellers(int index)
    {
        sellers[index].gameObject.SetActive(true);
    }
    public void UpgadeSeller(int index)
    {
        gm.AddMoney(-sellers[index].priceUpgrade);
        sellers[index].speed *= 1.5f;
        sellers[index].priceUpgrade *= 2;
        ui.upgradeSellerText[index].text = sellers[index].priceUpgrade.ToString();
    }
    public void AddBascet(int index)
    {
        maxBulletTypeCount[index] += maxFlowers;
    }
    public void setRadius()
    {
         ParticleSystem.ShapeModule shape =  radiusSphere.shape;
    }
    public void AddFlowers(int index, int count)
    {
        bulletTypeCount[index] += count;
        if (bulletTypeCount[index] > maxBulletTypeCount[index])
        {
            bulletTypeCount[index] = maxBulletTypeCount[index];
        }
        ui.flowrCounters[index].fillAmount = 1 / (float)maxBulletTypeCount[index] * bulletTypeCount[index];
    }
}
