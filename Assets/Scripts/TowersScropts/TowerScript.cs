using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{  
    private GameManager gm;
    private float boostTimer;
    private float boostReloadTimer;
    private Text bTimertext;
    [SerializeField] private GameObject upgradeTower;
    [SerializeField] private Button boostButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text destroyText;
    [SerializeField] private int priceOfUpgrade;
    [SerializeField] private int sellPrise;
    [SerializeField] private float boostTime;
    [SerializeField] private float boostReloadTime;
    public Sprite towerIco;
    public int priceTower;
    public float speed;
    public float radius;
    public ParticleSystem radiusSphere;
    public bool boostOn;
    // Start is called before the first frame update
    void Start()
    {
        boostButton.interactable = false;
        bTimertext = boostButton.GetComponentInChildren<Text>();
        upgradeButton.GetComponentInChildren<Text>().text = priceOfUpgrade.ToString();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        destroyText.text = sellPrise.ToString();
        boostOn = false;
        setRadius();
        boostReloadTimer = Time.time;
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
        if (gm.moneyCount >= priceOfUpgrade)
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
        GameObject newTower = GameObject.Instantiate(upgradeTower);
        newTower.transform.position = transform.position;
        gm.AddMoney(-priceOfUpgrade);
        Destroy(gameObject);
    }
    public void BoostActivate(bool active)
    {
        if (active)
        {
            boostOn = true;
            boostTimer = Time.time;
            boostButton.interactable = false;
            //boooost!
        }
        else
        {
            boostOn = false;
            boostReloadTimer = Time.time;
        }
        
    }
    public void setRadius()
    {
        ParticleSystem.ShapeModule shape = radiusSphere.shape;
        shape.radius = radius;
    }
    public void DestroyTower()
    {
        gm.AddMoney(sellPrise);
        GameObject empty = GameObject.Instantiate(gm.emptyForTower);
        empty.transform.position = transform.position;
        Destroy(gameObject);
    }
}
