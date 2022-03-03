using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIndicator : MonoBehaviour
{
    [SerializeField] private Sprite fillIndicator;
    [SerializeField] private Image[] indicators;
    private int upgradeCounter = 0;
    public void Upgrade()
    {
        indicators[upgradeCounter].sprite = fillIndicator;
        upgradeCounter++;
    }
}
