using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUIScript : MonoBehaviour
{
    [SerializeField] GameObject towerUI;
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
