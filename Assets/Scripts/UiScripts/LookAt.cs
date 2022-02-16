using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Transform target;
    void Start()
    {
        Quaternion quaternion = new Quaternion();
        quaternion.x = Camera.main.transform.rotation.x;
        quaternion.y = transform.rotation.y;
        quaternion.z = transform.rotation.z;
        target.rotation = quaternion;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
