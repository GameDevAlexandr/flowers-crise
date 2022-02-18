using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour
{
    [SerializeField] GameObject towerUI;
    public Image[] flowersCounters;
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
}
