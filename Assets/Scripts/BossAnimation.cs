using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesAnimation;
    [SerializeField] private float animationSpeed;
    [SerializeField] private bool noActive;
    private Image bossImage;
    private bool activateAnimation = false;
    private int frameCounter =0;
    private void Start()
    {
        Learn.bossActivate.AddListener(ActivateBoss);
        bossImage = GetComponent<Image>();
        ActivateBoss(!noActive);
    }
    public void ActivateBoss(bool isActive)
    {
        activateAnimation = isActive;
        if (isActive)
        {
            StartCoroutine(Animate());
        }
    }
    IEnumerator Animate()
    {
        yield return new WaitForSeconds(animationSpeed);
        if (activateAnimation || frameCounter != 0)
        {
            frameCounter++;
            if (frameCounter >= spritesAnimation.Length)
            {
                frameCounter = 0;
            }
            bossImage.sprite = spritesAnimation[frameCounter];
            StartCoroutine(Animate());
        }
       
    }
}
