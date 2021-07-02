using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform target;
    public float distance = 1.0f;
    float xMove;
    float yMove;
    
    //Ray hit;


    void Start()
    {
        target = GameObject.Find("Player").transform;        
    }

    void Update()
    {
        xMove += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(0, xMove, 0);

        if (Input.GetButton("Zoom"))
        {
            yMove += Input.GetAxisRaw("Mouse Y");
            transform.rotation = Quaternion.Euler(-yMove, xMove, 0);
        }
        //print(xMove + " " + yMove);
    }

    void LateUpdate()
    {

        if (!Input.GetButton("Zoom"))
        {
            yMove = 0;
            transform.position = target.transform.position - transform.rotation * new Vector3(0, -distance / 3, distance);
        }
        else
        {
            //transform.rotation = target.transform.rotation;                      
            transform.position = target.transform.position + transform.rotation * new Vector3(0, 0.5f, 0);            
        }

        Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }

}
