using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Difficulty;

public class GameManager : MonoBehaviour
{
    private TowerScript selectedTower;
    public void onToutchEvent(Vector3 position)
    {
        RaycastHit rcHit = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rcHit) && !EventSystem.current.IsPointerOverGameObject())
        {
             
            if (rcHit.transform.tag == "Tower")
            {
                selectedTower = rcHit.transform.GetComponent<TowerScript>();
                selectedTower.ActivateUI(true);
            }
            else if (selectedTower != null)
            {
                selectedTower.ActivateUI(false);
            }
        }
    }
}
