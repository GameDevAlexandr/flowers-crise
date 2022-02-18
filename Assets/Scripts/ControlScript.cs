using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    private GameManager gm;
    void Start()
    {
        gm = GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            gm.onToutchEvent(touch.position);
        }
    }
}
