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
    PlayerState ps;

    GameManger gm;
    public GameObject BossTracer;
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
    public bool check = false;

    Animation am;

    // JY 아이템 상호작용 관련 변수
    bool iDown;
    public GameObject nearObject;
    public GameObject[] Items;
    public GameObject[] Traces;
    public bool[] hasItem;
    public bool[] hasTrace;
    public bool isDodge;

    public bool gamecamOn = true;
    //



    Quaternion rotate;

    void Start()
    {
        ps = gameObject.GetComponent<PlayerState>();
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();
        am = gameObject.GetComponent<Animation>();

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
        if (PlayerState.stunCheck == true)
        {
            xInput = 0;
            zInput = 0;
        }

        move = new Vector3(xInput, 0, zInput);
        move.Normalize();

        if (gamecamOn != false)

        {
            move = Camera.main.transform.TransformDirection(move);
        }
        else
        {
            //esc 를 눌렀을때, 무브카메라 비활성화
        }
        move.y = 0;

        //  플레이어 이동
        if (check == false && PlayerFire.burstModeSubCheck == false)
        {
            cc.Move(bossMove * Time.deltaTime);
            cc.Move(move * moveSpeed * Time.deltaTime);
            //cc.Move(bossMove * Time.deltaTime);
            bossMove = Vector3.zero;
        }
        yVelocity += gravity * Time.deltaTime;
        cc.Move(new Vector3(0, yVelocity, 0));

        //  대쉬 기능
        if (Input.GetButton("Dash") && PlayerState.playerZoomCheck == false && ps.GetPlayerMP() >= 1.0f && PlayerFire.burstModeSubCheck == false)
        {
            if (!PlayerState.stunCheck == true)
            {
                moveSpeed = temp * 2;
                ps.AddPlayerMP(-Time.deltaTime * 4.0f);
            }

        }
        else
        {
            moveSpeed = temp;
        }
        #endregion

        //  플레이어 회전
        if (move != Vector3.zero && check == false)
        {
            if (PlayerState.playerZoomCheck == false && PlayerFire.burstModeSubCheck == false)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 25.0f * Time.deltaTime);
            }
            rotate = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
            //transform.rotation = rotate;
        }

        //  플레이어 점프
        if (Input.GetButtonDown("Jump") && check == false)
        {
            yVelocity = jumpPower;
        }

        //  플레이어 회피        
        if (time >= dodgeCooltime && check == false && move != Vector3.zero)
        {
            if (Input.GetButtonDown("Dodge") && ps.GetPlayerMP() >= 15.0f)
            {
                dodgeMove = move;
                check = true;
                transform.rotation = rotate;
                PlayerState.playerZoomCheck = false;
                PlayerFire.burstModeSubCheck = false;
                ps.AddPlayerMP(-15.0f);
                time = 0;

            }
        }
        if (check == true)
        {
            PlayerState.noHitCheck = true;
            cc.Move(dodgeMove * 10 * Time.deltaTime);
            Invoke("CheckOff", 0.217f);
        }

        //  줌 상태일 때 속도 제한.
        if (PlayerState.playerZoomCheck == true)
        {
            moveSpeed = temp / 2;
        }

        // 아이템 상호작용(F)
        if (Input.GetButtonDown("Interaction"))
        {

            if (nearObject != null)
            {

                if (nearObject.tag == "Item")
                {
                    Item item = nearObject.GetComponent<Item>();
                    int ItemIndex = item.value;
                    hasItem[ItemIndex] = true;

                    Destroy(nearObject);

                }
                else if (nearObject.tag == "Trace")
                {
                    Item Trace = nearObject.GetComponent<Item>();
                    int TraceIndex = Trace.value;
                    hasTrace[TraceIndex] = true;

                    Destroy(nearObject);
                    Vector3 dir = new Vector3(1, 1, 0);
                    BossTracer.transform.position = transform.position + dir;
                    Instantiate(BossTracer);

                }

            }
        }
    }

    //  체크 Off 함수
    void CheckOff()
    {
        check = false;
        PlayerState.noHitCheck = false;
    }

    private void OnTriggerStay(Collider other)
    {

        //  보스에게 밀려나는 판정
        if (other.gameObject.tag.Equals("Boss"))
        {
            bossMove = new Vector3(bF.move.x, 0, bF.move.z) * bF.bossSpeed;
        }

        // 아이템 트리거
        if (other.tag == "Item")
        {
            nearObject = other.gameObject;
        }
        // 흔적 트리거
        if (other.tag == "Trace")
        {
            nearObject = other.gameObject;
        }
    }

    void Gamecamoff()
    {
        gamecamOn = false;
    }

}
