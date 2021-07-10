using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    #region 보스 이동 변수
    public Vector3 move;
    public float bossSpeed = 1.0f;
    #endregion

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
    public float bossAttackDistance = 1.5f;
    #endregion

    float distance;
    GameObject target;
    PlayerState targetState;

    #region 보스 중력 변수
    float gravity = -0.8f;
    float yVelocity;
    #endregion

    #region 보스 패턴1 변수
    float p1X;
    float p1Y;
    float p1Time;
    Vector3 p1BossPos;
    Vector3 p1TargetPos;
    Vector3 p1HandlePos1;
    Vector3 p1HandlePos2;
    float p1Radius = 3.0f;
    public GameObject p1Bullet;
    #endregion

    CharacterController cc;

    float time;

    //  보스 FSM
    public enum State
    {
        Idle,
        Move,
        Attack,
        Pattern_4,
        Dead
    }
    public State bossState;
    void Start()
    {
        target = GameObject.Find("Player");
        targetState = target.GetComponent<PlayerState>();

        time = bossAttackCooltime;
        HP = maxBossHP;
        bossState = State.Move;

        cc = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {

        yVelocity += gravity * Time.deltaTime;

        cc.Move(new Vector3(0, yVelocity, 0));

        distance = (target.transform.position - transform.position).magnitude;

        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.O))
        {
            Pattern_1();
            
        }
        //if(distance <= attackDistance && bossState != State.Attack)
        //{
        //    print("보스 상태 전환 Attack");
        //    bossState = State.Attack;
        //}
        //else if(distance > attackDistance && bossState != State.Idle)
        //{
        //    print("보스 상태 전환 Idle");
        //    bossState = State.Idle;
        //}
        
        switch(bossState)
        {
            case State.Idle:
                LookTarget();
                break;
            case State.Move:
                LookTarget();
                move = new Vector3(target.transform.position.x - transform.position.x, 0, 
                    target.transform.position.z - transform.position.z);
                move.Normalize();
                cc.Move(move * bossSpeed * Time.deltaTime);
                break;
            case State.Attack:
                LookTarget();
                if(time >= bossAttackCooltime)
                {
                    BossAttack(bossAttackValue, bossDealStunValue);
                    time = 0;
                }                
                break;
            case State.Pattern_4:
                LookTarget();
                break;

            case State.Dead:
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

    void Pattern_1()
    {
        p1BossPos = transform.position + transform.up;        

        for (int i = 1; i <= 10; i++)
        {
            p1TargetPos = target.transform.position - target.transform.up * 1.5f +
                transform.TransformDirection(Random.Range(-2.0f + i * 0.2f, 2.0f - i * 0.2f), 0,
                    Random.Range(-2.5f + i * 0.5f, 7.5f - i * 0.5f));

            p1X = 5 * p1Radius * Mathf.Cos(Mathf.Deg2Rad * Random.Range(180, 360));
            p1Y = 5 * p1Radius * Mathf.Sin(Mathf.Deg2Rad * Random.Range(180, 360));

            p1HandlePos1 = p1BossPos + transform.TransformDirection(new Vector3(p1X, Random.Range(6.0f, 12.0f), p1Y));

            p1X = 2 * p1Radius * Mathf.Cos(Mathf.Deg2Rad * Random.Range(0, 360));
            p1Y = 2 * p1Radius * Mathf.Sin(Mathf.Deg2Rad * Random.Range(0, 360));

            p1HandlePos2 = p1TargetPos + new Vector3(p1X, 0.8f, p1Y);

            GameObject go = Instantiate(p1Bullet);
            go.GetComponent<P1BulletMove>().GetHandle(p1BossPos, p1TargetPos, p1HandlePos1, p1HandlePos2);
        }
    }

    private void OnDestroy()
    {
        
    }

}
