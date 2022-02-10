using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{  
    private GameManager gm;
    private float boostTimer;
    private bool boostOn;
    [SerializeField] private GameObject towerUI;
    [SerializeField] private GameObject upgradeTower;
    [SerializeField] private int priceOfUpgrade;
    [SerializeField] private int priceOfBoost;
    [SerializeField] private float boostTime;
    [SerializeField] private float speedAttack;
    [SerializeField] private float radius;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        boostOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (boostOn)
        {
            Boosting();
        }
    }
    public void ActivateUI(bool isActive)
    {
        if (isActive)
        {
            towerUI.SetActive(true);
        }
        else
        {
            towerUI.SetActive(false);
        }        
    }
    public void UpgradeTower()
    {
        GameObject newTower = GameObject.Instantiate(upgradeTower);
        newTower.transform.position = transform.position;
        Destroy(gameObject);
    }
    public void BoostActivate()
    {
        boostOn = true;
        boostTimer = Time.time;
    }
    private void Boosting()
    {
        if (Time.time - boostTimer < boostTime)
        {
            //boosting process
        }
        else
        {
            boostOn = false;
        }
    }

}
