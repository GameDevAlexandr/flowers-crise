using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour
{
    [SerializeField] GameObject otherUI;
    [SerializeField] private bool isActiveLearn;
    public GameObject towerUI;
    public ParticleSystem radiusSphere;
    public Image[] flowersCounters;
    private Learn learn;
    private void Start()
    {
        if (isActiveLearn)
        {
            learn = GetComponent<Learn>();
        }
    }
    public void ActivateUI(bool isActive)
    {
        if (isActive)
        {
            towerUI.SetActive(isActive);
            if (isActiveLearn)
            {
                isActiveLearn = false;
            }
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
        if (otherUI != null)
        otherUI.SetActive(true);
    }
}
