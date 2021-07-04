using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 마우스 입력값을 이용해서 회전하고 싶다.

public class CameraRotate : MonoBehaviour
{

    float rx;
    float ry;
    public float rotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 1. 마우스 입력값을 이용해서 

        float mx = Input.GetAxis("Mouse X");

        float my = Input.GetAxis("Mouse Y");

        rx += my * rotSpeed * Time.deltaTime;
        ry += my * rotSpeed * Time.deltaTime;

        // print(mx + ","+ my);
        // 회전하고 싶다.

        //rx의 회전 값을 제한하고 싶다. +80도 ~ -80도

        rx = Mathf.Clamp(rx, -80, 80);

        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
