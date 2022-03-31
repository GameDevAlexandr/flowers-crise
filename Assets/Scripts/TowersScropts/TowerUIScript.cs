using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour
{
    [SerializeField] GameObject otherUI;
    public GameObject towerUI;
    public ParticleSystem radiusSphere;
    public Image[] flowersCounters;
    public static UnityEvent firstClickEvent = new UnityEvent();
    private void Start()
    {
    }
    public void ActivateUI(bool isActive)
    {
        if (isActive)
        {
            towerUI.SetActive(isActive);
            if (otherUI != null)
            {
                otherUI.SetActive(false);
            }
            firstClickEvent.Invoke();
        }
        else
        {
            if(gameObject.activeSelf)
            StartCoroutine(DiactivateMenu());
        }
        
        if(radiusSphere!=null)
        radiusSphere.gameObject.SetActive(isActive);
      
    }
    IEnumerator DiactivateMenu()
    {
        yield return new WaitForSeconds(0.2f);
        towerUI.SetActive(false);
        if (otherUI != null)
        otherUI.SetActive(true);
    }
}
