using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    int playerState = 0;

    #region 플레이어 체력 변수
    public float playerMaxHP = 100.0f;
    float playerHP;
    float playerStunGauge = 0;
    #endregion

    #region 플레이어 상태 체크 변수    
    public static bool stunCheck = false;
    public static bool poisonCheck = false;
    public static bool sleepCheck = false;
    public static bool noHitCheck = false;
    #endregion

    GameManger gm;
    public BossFSM bF;

    //  0b_1110 스턴   0b_1101 독    0b_1011 수면
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

        #region 플레이어 스턴
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
        print("상태 되돌아옴");
    }
    
    void PlayerStun(int value)
    {
        print("스턴걸림");
        stunCheck = true;
        gm.stunEffect.gameObject.SetActive(true);
        
        StartCoroutine(ReturnState(value));
    }

    void PlayerPoison()
    {
        print("독걸림");
    }

    void PlayerSleep()
    {
        print("수면걸림");
    }

    //  플레이어 무적 상태 On 함수
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
                    print("패턴2 맞았음");
                    bF.p2ColType = false;
                }
            }
        }
    }

}
