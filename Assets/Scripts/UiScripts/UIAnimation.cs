using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesAnimation;
    [SerializeField] private float animationSpeed;
    private Image animationCanvas;
    private int frameCounter = 0;
    private void Start()
    {
       animationCanvas = GetComponent<Image>();
        //ActivateAnimation();
    }
    public void ActivateAnimation()
    {
       StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        yield return new WaitForSeconds(animationSpeed);
            frameCounter++;
            if (frameCounter < spritesAnimation.Length)
            {
                animationCanvas.sprite = spritesAnimation[frameCounter];
                animationCanvas.SetNativeSize();
                StartCoroutine(Animate());
            }            
    }
}
