using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour
{
    [SerializeField] GameObject towerUI;
    [SerializeField] GameObject otherUI;
    public ParticleSystem radiusSphere;
    public Image[] flowersCounters;
    public void ActivateUI(bool isActive)
    {
        towerUI.SetActive(isActive);
        
        if(radiusSphere!=null)
        radiusSphere.gameObject.SetActive(isActive);

        if (otherUI!=null)
        otherUI.SetActive(!isActive);        
    }
}
