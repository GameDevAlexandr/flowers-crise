using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private bool itsMobile;
    void Start()
    {
        gm = GetComponent<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (itsMobile)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                gm.onToutchEvent(touch.position);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                gm.onToutchEvent(Input.mousePosition);
            }
        }

    }
}
