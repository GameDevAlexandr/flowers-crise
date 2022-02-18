
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyForBuildScript : MonoBehaviour
{
    [SerializeField] private GameObject[] towersType;
    [SerializeField] private Button[] towersButton;
    private GameManager gm;
    private int[] towersPrice;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        towersPrice = new int[towersType.Length];
        for (int i = 0; i <  towersType.Length; i++)
        {
            TowerScript ts = towersType[i].GetComponent<TowerScript>();
            towersPrice[i] = ts.priceTower;
            towersButton[i].image.sprite = ts.towerIco;
            towersButton[i].GetComponentInChildren<Text>().text = ts.priceTower.ToString();
        }
    }
    void Update()
    {
        for (int i = 0; i < towersButton.Length; i++)
        {
            if (towersPrice[i] <= gm.moneyCount)
            {
                towersButton[i].interactable = true;
            }
            else
            {
                towersButton[i].interactable = false;
            }
        }
    }
    public void Building(int towerNumber)
    {
        GameObject newBuild = GameObject.Instantiate(towersType[towerNumber]);
        newBuild.transform.position = transform.position;
        if (towersType[towerNumber].name != "GreenHouse")
        {
            gm.marketInScene.Add(newBuild.GetComponent<FlowersMarketScript>());
            gm.updateTowers();
        }
        else
        {
            gm.greenHouseinScene.Add(newBuild.GetComponent<GreenHouseScript>());
        }
        Destroy(gameObject);
    }
}
