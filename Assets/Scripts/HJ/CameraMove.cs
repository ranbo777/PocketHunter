using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform target;

    public Vector3 offset = new Vector3(0, 1.1f, -1.1f);
    Ray hit;


    void Start()
    {
        target = GameObject.Find("Player").transform;
        
    }

    void Update()
    {        
            transform.RotateAround(target.position, Vector3.up, Input.GetAxisRaw("Mouse X"));
            transform.RotateAround(target.position, Vector3.right, -Input.GetAxisRaw("Mouse Y"));

        offset = transform.position - target.transform.position;
        offset.Normalize();

        Debug.DrawRay(transform.position, transform.forward, Color.blue);


    }

    void LateUpdate()
    {

        transform.position = target.transform.position + offset * 2.0f;
        transform.LookAt(target);
    }
}
