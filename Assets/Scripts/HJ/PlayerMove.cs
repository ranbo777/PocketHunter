using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //  �÷��̾� �̵� ��ũ��Ʈ
    //  ����Ű : W,A,S,D

    public float moveSpeed = 1.0f;
    BossFSM bF;
    CharacterController cc;
    PlayerState ps;

    GameManger gm;
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

    //  ȸ�� ��Ÿ�� �� ȸ�� �� �ٸ� �ൿ�� �����ϴ� üũ ����
    public bool check = false;

    Animation am;

    // JY ������ ��ȣ�ۿ� ���� ����
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

        #region �÷��̾� �̵�
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
            //esc �� ��������, ����ī�޶� ��Ȱ��ȭ
        }
        move.y = 0;

        //  �÷��̾� �̵�
        if (check == false && PlayerFire.burstModeSubCheck == false)
        {
            cc.Move(bossMove * Time.deltaTime);
            cc.Move(move * moveSpeed * Time.deltaTime);
            //cc.Move(bossMove * Time.deltaTime);
            bossMove = Vector3.zero;
        }
        yVelocity += gravity * Time.deltaTime;
        cc.Move(new Vector3(0, yVelocity, 0));

        //  �뽬 ���
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

        //  �÷��̾� ȸ��
        if (move != Vector3.zero && check == false)
        {
            if (PlayerState.playerZoomCheck == false && PlayerFire.burstModeSubCheck == false)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 25.0f * Time.deltaTime);
            }
            rotate = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
            //transform.rotation = rotate;
        }

        //  �÷��̾� ����
        if (Input.GetButtonDown("Jump") && check == false)
        {
            yVelocity = jumpPower;
        }

        //  �÷��̾� ȸ��        
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

        //  �� ������ �� �ӵ� ����.
        if (PlayerState.playerZoomCheck == true)
        {
            moveSpeed = temp / 2;
        }

        // ������ ��ȣ�ۿ�(F)
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

                }

            }
        }
    }

    //  üũ Off �Լ�
    void CheckOff()
    {
        check = false;
        PlayerState.noHitCheck = false;
    }

    private void OnTriggerStay(Collider other)
    {

        //  �������� �з����� ����
        if (other.gameObject.tag.Equals("Boss"))
        {
            bossMove = new Vector3(bF.move.x, 0, bF.move.z) * bF.bossSpeed;
        }

        // ������ Ʈ����
        if (other.tag == "Item")
        {
            nearObject = other.gameObject;
        }
        // ���� Ʈ����
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
