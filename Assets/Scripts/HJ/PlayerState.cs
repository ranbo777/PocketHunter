using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    int stateType;
    int playerState = 0;

    public float playerMaxHP = 100.0f;
    float playerHP;
    float playerStunGauge = 0;

    PlayerFire pf;
    PlayerMove pm;
    CameraMove cm;
    GameManger gm;

    public enum PlayerDebuff
    {
        Stun = 1,
        Sleep = 1<<1,
        Poison = 1<<2,

    }

    void Start()
    {
        playerHP = playerMaxHP;

        cm = Camera.main.GetComponent<CameraMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManger>();
        pm = gameObject.GetComponent<PlayerMove>();
        pf = gameObject.GetComponentInChildren<PlayerFire>();
        playerState = 0b_111;
    }

    void Update()
    {
        playerHP = Mathf.Max(0, playerHP);

        #region 플레이어 스턴
        if (playerStunGauge >= 5.0f)
        {
            if((playerState & 0b_001) == 0b_001)
            {
                playerState -= 0b_001;
                PlayerStun(0b_001);
                playerStunGauge = 0;               
            }
        }
        if((playerState & 0b_001) == 0b_000)
        {
            playerStunGauge = 0;
        }
        #endregion

    }
    IEnumerator ReturnState(int value)
    {
        yield return new WaitForSeconds(3.0f);
        pm.enabled = true;
        pf.enabled = true;
        cm.enabled = true;
        gm.stunEffect.gameObject.SetActive(false);
        playerState += value;
        print("상태 되돌아옴");
    }
    
    void PlayerStun(int value)
    {
        print("스턴걸림");
        pm.enabled = false;
        pf.enabled = false;
        cm.enabled = false;
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
