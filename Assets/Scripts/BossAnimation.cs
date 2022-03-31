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
    private int invokeCounter;
    private float deactivateTimer;
    private void Start()
    {
        Learn.bossActivate.AddListener(ActivateBoss);
        bossImage = GetComponent<Image>();
        ActivateBoss(!noActive);
        deactivateTimer = float.MaxValue;
    }
    public void ActivateBoss(bool isActive)
    {
        activateAnimation = isActive;
        if (isActive)
        {
            invokeCounter++;
            StartCoroutine(Animate(invokeCounter));
            deactivateTimer = float.MaxValue;
        }
    }
    IEnumerator Animate(int ic)
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
            StartCoroutine(Animate(ic));
        }
        deactivateTimer = Time.time; 
    }
    private void Update()
    {
        if(Time.time - deactivateTimer > 3)
        {
            transform.parent.gameObject.SetActive(false);
            deactivateTimer = float.MaxValue;
        }
    }
}
