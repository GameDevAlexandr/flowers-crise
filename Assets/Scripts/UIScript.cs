using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private GameManager gm;
    [HideInInspector] public Text moneyText;
    [HideInInspector] public Text pagesText;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        pagesText = GameObject.Find("PagesText").GetComponent<Text>();
        
    } 
   

}
