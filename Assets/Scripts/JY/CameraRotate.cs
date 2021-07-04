using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���콺 �Է°��� �̿��ؼ� ȸ���ϰ� �ʹ�.

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

        // 1. ���콺 �Է°��� �̿��ؼ� 

        float mx = Input.GetAxis("Mouse X");

        float my = Input.GetAxis("Mouse Y");

        rx += my * rotSpeed * Time.deltaTime;
        ry += my * rotSpeed * Time.deltaTime;

        // print(mx + ","+ my);
        // ȸ���ϰ� �ʹ�.

        //rx�� ȸ�� ���� �����ϰ� �ʹ�. +80�� ~ -80��

        rx = Mathf.Clamp(rx, -80, 80);

        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
