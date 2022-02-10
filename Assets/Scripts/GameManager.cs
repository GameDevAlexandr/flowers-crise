using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Difficulty;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject greenHouse;
    [SerializeField] private GameObject market;
    [SerializeField] private GameObject promotionTower;
    [SerializeField] private GameObject shoper;
    [SerializeField] private GameObject richShoper;
    [SerializeField] private GameObject wholesaler;
    [SerializeField] private int moneyCount;
    [SerializeField] private int pagesCount;
    private UIScript ui;
    private TowerScript selectedTower;
    private void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UIScript>();
        ui.moneyText.text = moneyCount.ToString();
        ui.pagesText.text = pagesCount.ToString();
    }
    public void onToutchEvent(Vector3 position)
    {

        RaycastHit rcHit = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(position), out rcHit))
        {
            Debug.Log(rcHit.transform.tag);
            if (rcHit.transform.tag == "Tower")
            {
                if(selectedTower!=null && selectedTower.transform != rcHit.transform)
                {
                    selectedTower.ActivateUI(false);
                }
                selectedTower = rcHit.transform.GetComponent<TowerScript>();
                selectedTower.ActivateUI(true);
            }
            else if (selectedTower != null && !EventSystem.current.IsPointerOverGameObject())
            {                
                selectedTower.ActivateUI(false);
            }
        }
    }
    public void AddMoney(int money)
    {
        moneyCount += money;
        ui.moneyText.text = moneyCount.ToString();
    }
    public void FlipPage(int count)
    {
        pagesCount -= count;
        ui.pagesText.text = pagesCount.ToString();
    }
}
