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
    #endregion

    GameManger gm;

    //  0b_001 ����   0b_010 ��    0b_100 ����
    void Start()
    {
        playerHP = playerMaxHP;

        gm = GameObject.Find("GameManager").GetComponent<GameManger>();
        playerState = 0b_111;
    }

    void Update()
    {
        playerHP = Mathf.Max(0, playerHP);

        #region �÷��̾� ����
        if (playerStunGauge >= 5.0f)
        {
            if((playerState & 0b_001) == 0b_001)
            {
                playerState -= 0b_001;
                PlayerStun(0b_001);
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
        if(value == 0b_001)
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
    public void SetStunGauge(float value)
    {
        playerStunGauge += value;
    }
    public void SetPlayerHP(float value)
    {
        playerHP -= value;
    }
    public float GetPlayerHP()
    {
        return playerHP;
    }

}
