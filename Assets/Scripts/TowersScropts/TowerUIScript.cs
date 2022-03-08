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
        if (isActive)
        {
            towerUI.SetActive(isActive);
        }
        else
        {
            if(gameObject.activeSelf)
            StartCoroutine(DiactivateMenu());
        }
        
        if(radiusSphere!=null)
        radiusSphere.gameObject.SetActive(isActive);

        if (otherUI!=null)
        otherUI.SetActive(!isActive);        
    }
    IEnumerator DiactivateMenu()
    {
        yield return new WaitForSeconds(0.1f);
        towerUI.SetActive(false);
    }
}
