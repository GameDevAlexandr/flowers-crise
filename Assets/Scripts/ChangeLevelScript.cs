using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameDataScript;

public class ChangeLevelScript : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private GameObject easyStar, middleStar, hardStar;
    private void Awake()
    {
        switch (levelRange[level])
        {
            case 1: easyStar.SetActive(true);
                    break; 
            case 2: middleStar.SetActive(true);
                    break;
            case 3: hardStar.SetActive(true);
                    break;
            default: 
                easyStar.SetActive(false);
                middleStar.SetActive(false);
                hardStar.SetActive(false);
                break;
        }
    }
    private void OnMouseDown()
    {
        if (level - 1 <= finishLevel)
        {
            SceneManager.LoadScene(level + 1);
        }
    }
}
