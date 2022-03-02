using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersMarketScript : MonoBehaviour
{  
    public int[] flowersType;
    public bool itsWineMarket;
    public float wineStrenght;
    public float wineActionTime;
    [HideInInspector] public int needFlowerType;
    [HideInInspector] public bool used;
    [SerializeField] private int[] maxFlowers;
    [SerializeField] private int dropCountFlowers;
    [SerializeField] GameObject shotElement;
    [SerializeField] GameObject[] seller;
    private TowerScript ts;
    private bool boostOn;
    private TowerUIScript tUI;
    // Start is called before the first frame update
    void Start()
    {
        tUI = GetComponent<TowerUIScript>();
        shotElement = GameObject.Instantiate(shotElement, transform.position, Quaternion.identity);
        ts = GetComponent<TowerScript>();
        boostOn = false;
        for (int i = 0; i < tUI.flowersCounters.Length; i++)
        {
            if(tUI.flowersCounters[i]!=null)
            tUI.flowersCounters[i].fillAmount =  1 / (float)maxFlowers[i] * flowersType[i];
        }
        if (itsWineMarket)
        {
            needFlowerType = 3;
            ts.gm.sounds.buildWineMarket.Play();
        }
        else
        {
            ts.gm.sounds.buildFloswerMarket.Play();   
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        if (ts.isUpgrade)
        {
            Upgrade();
        }
    }
    public void GetFlowers(int index, int count)
    {
        if (flowersType[index] + count > maxFlowers[index])
        {
            count = maxFlowers[index] - flowersType[index];
        }
        flowersType[index] += count;
        if(tUI.flowersCounters[index]!=null)
        tUI.flowersCounters[index].fillAmount = 1 / (float)maxFlowers[index] * flowersType[index];
    }
    public void SetNeededFlower(int flowerNeed)
    {
        needFlowerType = flowerNeed;
    }
    public void Upgrade()
    {
        ts.isUpgrade = false;
        if (!itsWineMarket)
        {
            seller[ts.levelTower].SetActive(true);
        }
        else
        {
            wineActionTime += 3;
        }
        for (int i = 0; i < maxFlowers.Length; i++)
        {
            maxFlowers[i] += maxFlowers[i];
            GetFlowers(i, 0);
        }
        tUI.ActivateUI(false);
    }
    public void Boosting(bool active)
    {

    }

}
