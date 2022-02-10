using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private GameObject towerUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
