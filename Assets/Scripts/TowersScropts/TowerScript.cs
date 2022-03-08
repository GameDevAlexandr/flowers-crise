using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{        
    public Sprite towerIco;
    public int priceTower;
    public float speed;
    public float radius;  
    [HideInInspector]public bool boostOn;
    [HideInInspector] public GameManager gm;
    [HideInInspector] public bool isUpgrade;
    [HideInInspector] public int levelTower;
    [HideInInspector] public GameObject empty;
    [SerializeField] private Button boostButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text destroyText;
    [SerializeField] private int priceOfUpgrade;
    [SerializeField] private int sellPrise;
    [SerializeField] private float boostTime;
    [SerializeField] private float boostReloadTime;
    [SerializeField] private int maxUpgardeLevel;
    [SerializeField] bool itsMarket;
    [SerializeField] ParticleSystem effectBoost;
    private ParticleSystem radiusSphere;
    private Text priceUpgradeText;
    private float boostTimer;
    private float boostReloadTimer;
    private Text bTimertext;
    private TowerUIScript tUI;
    // Start is called before the first frame update
    void Start()
    {
        levelTower = 1;
        boostButton.interactable = false;
        bTimertext = boostButton.GetComponentInChildren<Text>();
        priceUpgradeText = upgradeButton.GetComponentInChildren<Text>();
        priceUpgradeText.text = priceOfUpgrade.ToString();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        destroyText.text ="+"+ sellPrise.ToString();
        boostOn = false;
        boostReloadTimer = Time.time;
        tUI = GetComponent<TowerUIScript>();
        radiusSphere = tUI.radiusSphere;
        setRadius();
    }

    // Update is called once per frame
    void Update()
    {
        //boosters
        if (boostOn)
        {
            bTimertext.text = ((int)(boostTime - (Time.time - boostTimer))).ToString();
            if (Time.time - boostTimer > boostTime)
            {
                BoostActivate(false);
            }
        }
        else
        {
            int timer = (int)(boostReloadTime - (Time.time - boostReloadTimer));
            if (timer >= 0)
            {
                bTimertext.text = ((int)(boostReloadTime - (Time.time - boostReloadTimer))).ToString();
            }
            if (Time.time - boostReloadTimer > boostReloadTime)
            {
                boostButton.interactable = true;
            }
        }
        //activate/diactivate upgrade button
        if (gm.moneyCount >= priceOfUpgrade && levelTower<maxUpgardeLevel)
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }
    public void UpgradeTower()
    {
        tUI.ActivateUI(false);
        levelTower ++;
        isUpgrade = true;
        gm.AddMoney(-priceOfUpgrade);
        gm.sounds.upgrade.Play();
        if (levelTower < maxUpgardeLevel)
        {
            priceOfUpgrade *= 2;
            sellPrise += sellPrise * 2;
            destroyText.text = sellPrise.ToString();
            priceUpgradeText.text = priceOfUpgrade.ToString();
        }
        else
        {
            priceUpgradeText.text = "";
        }
    }
    public void BoostActivate(bool active)
    {
        if (active)
        {
            tUI.ActivateUI(false);
            boostOn = true;
            gm.sounds.boost.Play();
            boostTimer = Time.time;
            boostButton.interactable = false;
            effectBoost?.Play();
        }
        else
        {
            boostOn = false;
            boostReloadTimer = Time.time;
            effectBoost?.Stop();
        }
        
    }
    public void setRadius()
    {
        ParticleSystem.ShapeModule shape = radiusSphere.shape;
        shape.radius = radius;
    }
    public void DestroyTower()
    {
        if (itsMarket)
        {
            gm.marketInScene.Remove(GetComponent<FlowersMarketScript>());
        }
        gm.AddMoney(sellPrise);
        gm.sounds.sellBuilding.Play();
        empty.SetActive(true);
        Destroy(gameObject);
    }
}
