using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Transform target;
    [SerializeField] private bool xAx;
    [SerializeField] private bool yAx;
    [SerializeField] private bool isDinamic;
    [SerializeField] private Vector3 targetDirect;
    void Start()
    {
        target = Camera.main.transform;
        //targetDirect = target.position;
        //if (!xAx)
        //{
        //    targetDirect.x = transform.position.x;
        //    target.transform.position = targetDirect;
        //}
        //if (!yAx)
        //{
        //    targetDirect.y = transform.position.y;
        //    target.transform.position = targetDirect;
        //}
        //transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDinamic)
        {
            transform.LookAt(target);
        }
    }
}
