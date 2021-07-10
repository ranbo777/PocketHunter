using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //  플레이어 이동 스크립트
    //  조작키 : W,A,S,D

    public float moveSpeed = 1.0f;
    BossFSM bF;
    CharacterController cc;

    float temp;
    float time = 0;

    float gravity = -0.8f;
    float jumpPower = 0.2f;
    float yVelocity = 0;

    float dodgeCooltime = 1.0f;
    Vector3 dodgeMove = Vector3.zero;

    float xInput;
    float zInput;

    Vector3 move;

    Vector3 bossMove = Vector3.zero;

    //  회피 쿨타임 및 회피 중 다른 행동을 제한하는 체크 변수
    bool check = false;



    Quaternion rotate;

    void Start()
    {
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();

        temp = moveSpeed;
        cc = gameObject.GetComponent<CharacterController>();
        time = dodgeCooltime;
    }


    void Update()
    {
        time += Time.deltaTime;

        #region 플레이어 이동
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        if(PlayerState.stunCheck == true)
        {
            xInput = 0;
            zInput = 0;
        }

        move = new Vector3(xInput, 0, zInput);
        move.Normalize();
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0;

        //  플레이어 이동
        if (check == false)
        {
            cc.Move(move * moveSpeed * Time.deltaTime);
            cc.Move(bossMove * 1.1f * Time.deltaTime);
            bossMove = Vector3.zero;
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
        if (move != Vector3.zero && check == false)
        {
            if (!Input.GetButton("Zoom"))
            {
                rotate = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
            }            
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 15.0f * Time.deltaTime);
        }

        //  플레이어 점프
        if (Input.GetButtonDown("Jump") && check == false)
        {
            yVelocity = jumpPower;
        }

        //  플레이어 회피        
        if (time >= dodgeCooltime && check == false)
        {
            if (Input.GetButtonDown("Dodge"))
            {
                dodgeMove = move;
                check = true;
                time = 0;
            }
        }
        if (check == true)
        {
            cc.Move(dodgeMove * 10 * Time.deltaTime);
            Invoke("CheckOff", 0.217f);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Boss"))
        {
            bossMove = new Vector3(bF.move.x, 0, bF.move.z);
        }
    }
}
