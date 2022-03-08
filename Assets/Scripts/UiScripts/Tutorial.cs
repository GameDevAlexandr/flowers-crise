using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image scroll;
    [SerializeField] private Image downHandle;
    [SerializeField] private Sprite[] tutorials;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button beforeButton;
    [SerializeField] private float speed;
    private int countTutorials;
    private float scrollHigth;
    private Vector2 startHandlePosition;
    private bool rolling;
    private GameManager gm;
    private void Awake()
    {
        scrollHigth = scroll.rectTransform.rect.height;
        countTutorials = 0;
        startHandlePosition = downHandle.rectTransform.localPosition;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rolling)
        {
            scroll.fillAmount += speed;
            Vector2 handlePosition = downHandle.rectTransform.localPosition;
            handlePosition.y -= scrollHigth * speed;
            downHandle.rectTransform.localPosition = handlePosition;
            if (scroll.fillAmount >= 1)
            {
                rolling=false;
                Time.timeScale =0;
            }
        }
    }
    public void ActivateScroll(bool next)
    {
        if (next)
        {
            countTutorials++;
            beforeButton.interactable = true;
            if(countTutorials == tutorials.Length-1)
            {
                nextButton.interactable = false;
            }
        }
        else
        {
            countTutorials--;
            nextButton.interactable = true;
            if (countTutorials == 0)
            {
                beforeButton.interactable = false;
            }
        }
        scroll.fillAmount = 0;
        downHandle.rectTransform.localPosition = startHandlePosition;
        scroll.sprite = tutorials[countTutorials];
        Time.timeScale =1;
        gm.sounds.bookOfComplaint.Play();
        rolling = true;
    }
    private void OnEnable()
    {
        downHandle.rectTransform.localPosition = startHandlePosition;
        scroll.fillAmount = 0;
        rolling = true;
        gm.sounds?.bookOfComplaint.Play();
    }
}
