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

    #region 플레이어 스태미너 변수
    public float playerMaxMP = 100.0f;
    float playerMP;
    float recoveryMPTime = 0;
    #endregion

    #region 플레이어 상태 체크 변수    
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

    //  0b_1110 스턴   0b_1101 독    0b_1011 수면
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
                if (playerZoomCheck == false && pm.check == false) { playerZoomCheck = true; print("줌 On"); }
                else { playerZoomCheck = false; print("줌 Off"); }
                time = 0;
            }
        }

        playerHP = Mathf.Max(0, playerHP);
        playerMP = Mathf.Max(0, playerMP);

        //  MP 회복
        recoveryMPTime += Time.deltaTime;
        if(recoveryMPTime >= 3.0f && playerMP < 100.0f)
        {
            playerMP += Time.deltaTime * 5.0f;
            playerMP = Mathf.Min(100, playerMP);
        }

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
        playerZoomCheck = false;
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

    #region 체력, 스태미나 관련 함수
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
                    print("패턴2 맞았음");
                    bF.p2ColType = false;
                }
            }
        }
    }

}
