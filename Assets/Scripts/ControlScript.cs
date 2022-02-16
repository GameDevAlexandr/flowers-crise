using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    private GameManager gm;
    private bool touches;
    void Start()
    {
        gm = GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log(Input.touchCount);
            Touch touch = Input.GetTouch(0);
            gm.onToutchEvent(touch.position, touch.phase);
        }
    }
}
