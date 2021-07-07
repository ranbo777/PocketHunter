using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    #region 보스 체력 변수
    public float maxBossHP = 10.0f;
    float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }
    #endregion

    #region 보스 공격 변수
    public float bossAttackValue = 20.0f;
    public float bossDealStunValue = 2.5f;
    public float bossAttackCooltime = 2.0f;
    public float attackDistance = 1.5f;
    #endregion

    float distance;
    GameObject target;
    PlayerState targetState;

    float gravity = -0.8f;
    float yVelocity;
    CharacterController cc;

    float time;

    //  보스 FSM
    public enum State
    {
        Idle,
        Move,
        Attack
    }
    public State bossState;
    void Start()
    {
        target = GameObject.Find("Player");
        targetState = target.GetComponent<PlayerState>();

        time = bossAttackCooltime;
        HP = maxBossHP;
        bossState = State.Idle;

        cc = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        yVelocity += gravity * Time.deltaTime;

        cc.Move(new Vector3(0, yVelocity, 0));

        distance = (target.transform.position - transform.position).magnitude;

        time += Time.deltaTime;

        if(distance <= attackDistance && bossState != State.Attack)
        {
            print("보스 상태 전환 Attack");
            bossState = State.Attack;
        }
        else if(distance > attackDistance && bossState != State.Idle)
        {
            print("보스 상태 전환 Idle");
            bossState = State.Idle;
        }
        

        switch(bossState)
        {
            case State.Idle:
                LookTarget();
                break;
            case State.Move:
                break;
            case State.Attack:
                LookTarget();
                if(time >= bossAttackCooltime)
                {
                    BossAttack(bossAttackValue, bossDealStunValue);
                    time = 0;
                }                
                break;
        }
        
        //  보스 hp가 0 밑으로 내려가지 않게 설정.
        HP = Mathf.Max(0, HP);
    }

    private void BossAttack(float value, float stunValue)
    {
        print("공격해쓰요");
        targetState.SetPlayerHP(value);
        targetState.SetStunGauge(stunValue);
    }

    public void TakeDamage(float p)
    {
        HP -= p;
    }

    void LookTarget()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }
}
