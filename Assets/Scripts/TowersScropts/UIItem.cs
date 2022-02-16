using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Button[] upgradeSellers;
    public Button[] addSellers;
    public Image[] flowrCounters;
    [HideInInspector] public Text[] upgradeSellerText;
    [HideInInspector] public Text[] addSellerText;
    private TowerScript ts;
    private void Awake()
    {
        ts = GetComponent<TowerScript>();
        upgradeSellerText = new Text[upgradeSellers.Length];
        addSellerText = new Text[addSellers.Length];
        for (int i = 0; i < 3; i++)
        {
            upgradeSellerText[i] = upgradeSellers[i].GetComponentInChildren<Text>();
            if (i < 2)
            {
                addSellerText[i] = addSellers[i].GetComponentInChildren<Text>();
            }
        }
    }
}
