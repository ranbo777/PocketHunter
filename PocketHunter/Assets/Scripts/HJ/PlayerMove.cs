using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //  플레이어 이동 스크립트
    //  조작키 : W,A,S,D

    public float moveSpeed = 1.0f;
    float temp;

    void Start()
    {
        temp = moveSpeed;
    }

    
    void Update()
    {
        #region 플레이어 이동
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        move.Normalize();
        move = Camera.main.transform.TransformDirection(move);

        transform.position += move * moveSpeed * Time.deltaTime;

        //  대쉬 기능
        if (Input.GetButton("Dash"))
        {
            moveSpeed = temp * 3;
        }
        else
        {
            moveSpeed = temp;
        }
        #endregion
        if (move != Vector3.zero)
        {
            Quaternion rotate = Quaternion.LookRotation(new Vector3(move.x, 0 , move.z));
            
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 10.0f*Time.deltaTime);
        }

    }
}
