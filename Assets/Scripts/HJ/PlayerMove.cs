using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //  플레이어 이동 스크립트
    //  조작키 : W,A,S,D

    public float moveSpeed = 1.0f;
    CharacterController cc;
    float temp;
    float time = 0;
    float gravity = -0.8f;
    float yVelocity = 0;
    float dodgeCooltime = 1.0f;
    float jumpPower = 0.2f;
    bool check = false;

    void Start()
    {
        temp = moveSpeed;
        cc = gameObject.GetComponent<CharacterController>();
        time = dodgeCooltime;
    }


    void Update()
    {
        time += Time.deltaTime;

        #region 플레이어 이동
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        move = Camera.main.transform.TransformDirection(move);
        move.Normalize();
        move.y = 0;

        //  플레이어 이동
        if (check == false)
        {
            cc.Move(move * moveSpeed * Time.deltaTime);
        }
        yVelocity += gravity * Time.deltaTime;
        cc.Move(new Vector3(0, yVelocity, 0));

        //  대쉬 기능
        if (Input.GetButton("Dash") && !Input.GetButton("Zoom"))
        {
            moveSpeed = temp * 2;
        }
        else
        {
            moveSpeed = temp;
        }
        #endregion

        //  플레이어 회전
        if (move != Vector3.zero)
        {
            Quaternion rotate = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 15.0f * Time.deltaTime);
        }

        //  플레이어 점프
        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        //  플레이어 회피
        if (time >= dodgeCooltime)
        {
            if (Input.GetButtonDown("Dodge"))
            {
                check = true;
                time = 0;
            }
        }
        if (check == true)
        {
            cc.Move(move * 15 * Time.deltaTime);
            Invoke("CheckOff", 0.12f);
        }
        //  줌 상태일 때 속도 제한.
        if (Input.GetButton("Zoom"))
        {
            moveSpeed = temp / 2;
        }
    }

    //  체크 Off 함수
    void CheckOff()
    {
        check = false;
    }
}
