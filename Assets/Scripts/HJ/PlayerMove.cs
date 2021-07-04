using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //  �÷��̾� �̵� ��ũ��Ʈ
    //  ����Ű : W,A,S,D

    public float moveSpeed = 1.0f;
    CharacterController cc;
    float temp;
    float yVelocity = 5.0f;

    void Start()
    {
        temp = moveSpeed;
        cc = gameObject.GetComponent<CharacterController>();
    }

    
    void Update()
    {
        #region �÷��̾� �̵�
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        move = Camera.main.transform.TransformDirection(move);
        move.Normalize();
        move.y = 0;


        cc.Move(move * moveSpeed * Time.deltaTime);
        cc.Move(new Vector3(0, -yVelocity, 0));

        //  �뽬 ���
        if (Input.GetButton("Dash") && !Input.GetButton("Zoom"))
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
            
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 15.0f*Time.deltaTime);
        }

        if (Input.GetButton("Zoom"))
        {
            moveSpeed = temp / 2;
        }        

    }
}
