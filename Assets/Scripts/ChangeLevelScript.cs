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
    [SerializeField] private UIAnimation[] loadAnimation;
    private Image loadProgress;
    private void Awake()
    {
        loadProgress = GameObject.Find("LoadProgress").GetComponent<Image>();
        ChangeRange();
        changeRange.AddListener(ChangeRange);
    }
    public void ChangeLevel()
    {
        StartCoroutine(LoadAsync(level + 1));
        //if (level - 1 <= finishLevel)
        //{
        //    SceneManager.LoadScene(level + 1);
        //}
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadProgress.fillAmount = progress;
            int animationIndex = (int)(progress * 9);
            loadAnimation[animationIndex].gameObject.SetActive(true);
            loadAnimation[animationIndex].ActivateAnimation();
            yield return null;
        }
        
    }
    private void ChangeRange()
    {
        switch (levelRange[level])
        {
            case 1:
                easyStar.SetActive(true);
                break;
            case 2:
                easyStar.SetActive(true);
                middleStar.SetActive(true);
                break;
            case 3:
                easyStar.SetActive(true);
                middleStar.SetActive(true);
                hardStar.SetActive(true);
                break;
            default:
                easyStar.SetActive(false);
                middleStar.SetActive(false);
                hardStar.SetActive(false);
                break;
        }
    }

}
