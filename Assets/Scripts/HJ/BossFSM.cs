using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    #region ���� ü�� ����
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

    #region ���� ���� ����
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

    //  ���� FSM
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
            print("���� ���� ��ȯ Attack");
            bossState = State.Attack;
        }
        else if(distance > attackDistance && bossState != State.Idle)
        {
            print("���� ���� ��ȯ Idle");
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
        
        //  ���� hp�� 0 ������ �������� �ʰ� ����.
        HP = Mathf.Max(0, HP);
    }

    private void BossAttack(float value, float stunValue)
    {
        print("�����ؾ���");
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
