using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    int playerState = 0;

    #region �÷��̾� ü�� ����
    public float playerMaxHP = 100.0f;
    float playerHP;
    float playerStunGauge = 0;
    #endregion

    #region �÷��̾� ���¹̳� ����
    public float playerMaxMP = 100.0f;
    float playerMP;
    float recoveryMPTime = 0;
    #endregion

    #region �÷��̾� ���� üũ ����    
    public static bool stunCheck = false;
    public static bool poisonCheck = false;
    public static bool sleepCheck = false;
    public static bool noHitCheck = false;
    public static bool playerZoomCheck = false;
    #endregion

    GameManger gm;
    PlayerMove pm;
    public BossFSM bF;

    float time = 0;

    //  0b_1110 ����   0b_1101 ��    0b_1011 ����
    void Start()
    {
        playerHP = playerMaxHP;
        playerMP = playerMaxMP;
        
        if(bF == null) { bF = GameObject.Find("Boss").GetComponent<BossFSM>(); }
        pm = gameObject.GetComponent<PlayerMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManger>();
        playerState = 0b_1111;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetButtonDown("Zoom"))
        {
            if (time >= 0.2f)
            {
                if (playerZoomCheck == false && pm.check == false) { playerZoomCheck = true; print("�� On"); }
                else { playerZoomCheck = false; print("�� Off"); }
                time = 0;
            }
        }

        playerHP = Mathf.Max(0, playerHP);
        playerMP = Mathf.Max(0, playerMP);

        //  MP ȸ��
        recoveryMPTime += Time.deltaTime;
        if(recoveryMPTime >= 3.0f && playerMP < 100.0f)
        {
            playerMP += Time.deltaTime * 5.0f;
            playerMP = Mathf.Min(100, playerMP);
        }

        #region �÷��̾� ����
        if (playerStunGauge >= 5.0f)
        {
            if((playerState & 0b_0001) == 0b_0001)
            {
                playerState -= 0b_0001;
                PlayerStun(0b_0001);
                playerStunGauge = 0;               
            }
        }
        if(stunCheck == true)
        {
            playerStunGauge = 0;
        }
        #endregion

    }
    IEnumerator ReturnState(int value)
    {
        yield return new WaitForSeconds(3.0f);
        gm.stunEffect.gameObject.SetActive(false);        
        playerState += value;
        if(value == 0b_0001)
        {
            stunCheck = false;
        }
        print("���� �ǵ��ƿ�");
    }
    
    void PlayerStun(int value)
    {
        print("���ϰɸ�");
        playerZoomCheck = false;
        stunCheck = true;
        gm.stunEffect.gameObject.SetActive(true);
        
        StartCoroutine(ReturnState(value));
    }

    void PlayerPoison()
    {
        print("���ɸ�");
    }

    void PlayerSleep()
    {
        print("����ɸ�");
    }

    //  �÷��̾� ���� ���� On �Լ�
    void PlayerNoHit()
    {
        noHitCheck = true;
    }

    public void AddStunGauge(float value)
    {
        playerStunGauge += value;
    }

    #region ü��, ���¹̳� ���� �Լ�
    public void AddPlayerHP(float value)
    {
        playerHP += value;
    }

    public float GetPlayerHP()
    {
        return Mathf.Max(0, playerHP);
    }

    public void AddPlayerMP(float value)
    {
        playerMP += value;
        recoveryMPTime = 0;
    }

    public float GetPlayerMP()
    {
        return Mathf.Max(0, playerMP);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("BossAttackCol"))
        {
            if(noHitCheck == false)
            {
                if (bF.p2ColType == true)
                {
                    AddPlayerHP(-bF.p2AttackValue);
                    AddStunGauge(2.5f);
                    print("����2 �¾���");
                    bF.p2ColType = false;
                }
            }
        }
    }

}
