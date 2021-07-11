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

    #region �÷��̾� ���� üũ ����    
    public static bool stunCheck = false;
    public static bool poisonCheck = false;
    public static bool sleepCheck = false;
    public static bool noHitCheck = false;
    #endregion

    GameManger gm;
    public BossFSM bF;

    //  0b_1110 ����   0b_1101 ��    0b_1011 ����
    void Start()
    {
        playerHP = playerMaxHP;
        
        if(bF == null) { bF = GameObject.Find("Boss").GetComponent<BossFSM>(); }
        gm = GameObject.Find("GameManager").GetComponent<GameManger>();
        playerState = 0b_1111;
    }

    void Update()
    {
        playerHP = Mathf.Max(0, playerHP);

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
    public void AddPlayerHP(float value)
    {
        playerHP += value;
    }
    public float GetPlayerHP()
    {
        return Mathf.Max(0, playerHP);
    }

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
