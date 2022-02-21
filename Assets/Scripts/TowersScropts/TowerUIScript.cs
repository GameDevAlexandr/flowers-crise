using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour
{
    [SerializeField] GameObject towerUI;
    [SerializeField] GameObject otherUI;
    public Image[] flowersCounters;
    public void ActivateUI(bool isActive)
    {
        if (isActive)
        {
            towerUI.SetActive(true);
            if(otherUI!=null)
            otherUI.SetActive(false);
        }
        else
        {
            towerUI.SetActive(false);
            if(otherUI != null)
            otherUI.SetActive(true);
        }
    }
}
