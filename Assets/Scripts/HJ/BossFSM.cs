using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    #region 보스 이동 변수
    public Vector3 move;
    public float bossSpeed = 5.0f;
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

    #region 보스 중력 변수
    float gravity = -0.8f;
    float yVelocity;
    #endregion

    #region 보스 경직 변수
    float bossGroggyTime = 0;
    float bossGroggyValue = 0;
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
    bool p1Check = true;
    #endregion

    #region 보스 패턴2 변수
    public float p2AttackValue = 10.0f;
    bool p2Check = true;
    [HideInInspector] public bool p2ColType = false;
    #endregion

    #region 보스 패턴3 변수
    bool p3Check = true;
    float p3MoveTime = 0;
    #endregion

    float distance;

    float time2 = 0;

    GameObject target;
    PlayerState targetState;
    CharacterController cc;
    Animator am;

    float time;

    public GameObject BossItem;
    public GameObject bossHpUI;
    

    //  보스 FSM
    public enum State
    {
        Idle,
        Move,
        Groggy,
        Pattern_1,
        Pattern_2,
        Pattern_3,
        Dead
    }
    public State bossState;
    void Start()
    {
        am = gameObject.GetComponent<Animator>();
        target = GameObject.Find("Player");
        targetState = target.GetComponent<PlayerState>();

        HP = maxBossHP;
        bossState = State.Idle;

        cc = gameObject.GetComponent<CharacterController>();
        
    }

    void Update()
    {

        yVelocity += gravity * Time.deltaTime;

        //cc.Move(new Vector3(0, yVelocity, 0));

        distance = (target.transform.position - transform.position).magnitude;

        //print(distance);

        time += Time.deltaTime;
        time2 += Time.deltaTime;

        //if (distance <= bossAttackDistance && bossState != State.Attack)
        //{
        //    print("보스 상태 전환 Attack");
        //    bossState = State.Attack;
        //}
        //else if (distance > bossAttackDistance && bossState != State.Idle)
        //{
        //    print("보스 상태 전환 Idle");
        //    bossState = State.Idle;
        //}

        if (bossGroggyValue >= 10.0f && bossState != State.Pattern_3)
        {
            bossState = State.Groggy;
            am.SetBool("OnGroggy", true);
            print("보스 경직");
        }


        //if (time2 >= 0.5f) { print(distance);  time2 = 0; }

        if(HP <= 0)
        {
            bossState = State.Dead;
        }

        switch(bossState)
        {
            case State.Idle:
                LookTarget();
                am.SetBool("OnShoot", false);
                am.SetFloat("Runspeed", 0);

                move = Vector3.zero;
                am.SetFloat("Runspeed", move.magnitude);

                if (distance <= 2.0f)
                {
                    p2Check = true;
                    bossState = State.Pattern_2;
                    print("패턴2 실행");
                }

                if (distance > 2.0f && distance <= 8.0f)
                {
                    int p = Random.Range(0, 100);
                    if (p >= 90)
                    {
                        p1Check = true;
                        bossState = State.Pattern_1;
                        print("패턴1 실행");
                    }
                    else { bossState = State.Move; }
                }

                if (distance > 8.0f)
                {
                    int p = Random.Range(0, 100);

                    if (p <= 20)
                    {
                        p1Check = true;
                        bossState = State.Pattern_1;
                        print("패턴1 실행");
                    }
                    else 
                    {
                        p3Check = true;
                        p3MoveTime = 0;
                        bossState = State.Pattern_3;
                        print("패턴3 실행");
                    }

                }

                //if (Input.GetKeyDown(KeyCode.O))
                //{
                //    p1Check = true;
                //    bossState = State.Pattern_1;
                //    print("패턴1 실행");
                //}
                //if (Input.GetKeyDown(KeyCode.P))
                //{
                //    p2Check = true;
                //    bossState = State.Pattern_2;
                //    print("패턴2 실행");
                //}
                break;
            case State.Move:
                LookTarget();
                move = new Vector3(target.transform.position.x - transform.position.x, 0, 
                    target.transform.position.z - transform.position.z);
                move.Normalize();
                am.SetFloat("Runspeed", move.magnitude);                
                cc.Move(move * bossSpeed * Time.deltaTime);
                if(distance <= 1.5f) { bossState = State.Idle; }                                
                break;
            case State.Groggy:
                bossGroggyTime += Time.deltaTime;
                if(bossGroggyTime >= 5.0f)
                {
                    bossGroggyValue = 0;
                    bossGroggyTime = 0;
                    am.SetBool("OnGroggy", false);
                    am.SetBool("OnIdle", true);
                    print("보스 경직 해제");
                }
                break;
            case State.Pattern_1:
                if(p1Check == true) 
                {
                    am.SetBool("OnShoot", true);
                    p1Check = false;
                    Invoke("ReturnState", 4.0f);
                }                
                break;
            case State.Pattern_2:
                if(p2Check == true)
                {
                    Pattern_2();
                    p2Check = false;
                    Invoke("ReturnState", 2.5f);                    
                }
                break;
            case State.Pattern_3:
                p3MoveTime += Time.deltaTime;
                move = new Vector3(target.transform.position.x - transform.position.x, 0,
                    target.transform.position.z - transform.position.z);                
                if(p3MoveTime >= 0.5f && p3MoveTime <= 1.5f)
                {
                    LookTarget();
                    cc.Move(move * 2.0f * Time.deltaTime);
                }                
                if (p3Check == true)
                {
                    p3Check = false;
                    Pattern_3();
                    Invoke("ReturnState", 5.0f);
                }
                break;
            case State.Dead:
                OnDestroyBoss();
                break;
        }        
        //  보스 hp가 0 밑으로 내려가지 않게 설정.
        HP = Mathf.Max(0, HP);
    }

    //  보스가 외부에서 데미지를 받는 함수.
    public void TakeDamage(float playerAttackValue, float playerGroggyValue)
    {
        HP -= playerAttackValue;
        bossGroggyValue += playerGroggyValue;
    }

    public void TakeMeleeDamage(float x)
    {
        HP -= x;
    }

    void LookTarget()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        
    }

    //  패턴1: 보스가 count 만큼의 투사체를 생성해 적에게 발사.
    IEnumerator Pattern_1(int count)
    {
        int count_ = count;
        while (count_ > 0 && bossState != State.Groggy)
        {
            count_--;
            p1BossPos = transform.position + transform.up;        
            p1TargetPos = target.transform.position - target.transform.up+
                transform.TransformDirection(Random.Range(-1.0f, 1.0f), 0,
                    Random.Range(-1.5f, 1.5f));

            p1X = 5 * p1Radius * Mathf.Cos(Mathf.Deg2Rad * Random.Range(180, 360));
            p1Y = 5 * p1Radius * Mathf.Sin(Mathf.Deg2Rad * Random.Range(180, 360));

            p1HandlePos1 = p1BossPos + transform.TransformDirection(new Vector3(p1X, Random.Range(6.0f, 12.0f), p1Y));

            p1X = 2 * p1Radius * Mathf.Cos(Mathf.Deg2Rad * Random.Range(0, 360));
            p1Y = 2 * p1Radius * Mathf.Sin(Mathf.Deg2Rad * Random.Range(0, 360));

            p1HandlePos2 = p1TargetPos + new Vector3(p1X, 1.0f, p1Y);

            GameObject go = Instantiate(p1Bullet);
            go.GetComponent<P1BulletMove>().GetHandle(p1BossPos, p1TargetPos, p1HandlePos1, p1HandlePos2);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;                            
    }

    //  패턴2: 보스가 근접 공격을 함
    void Pattern_2()
    {
        am.SetBool("OnAttack1", true);
        p2ColType = true;
    }

    void Pattern_3()
    {
        am.SetBool("OnJumpAttack", true);
    }

    //  보스가 패턴 사용 후 Idle 상태로 넘어갈 때 까지 걸리는 딜레이 함수
    void ReturnState()
    {
        bossState = State.Idle;
        print("패턴 딜레이 종료");
    }

    //보스 죽음
    void OnDestroyBoss()
    {
        Vector3 Revise = new Vector3(0, 1, 0);


        GameObject go = Instantiate(BossItem);
        go.SetActive(true);
        go.transform.position = transform.position - Revise;
        Destroy(gameObject);
        OffBossUI();
    }

    public void OffBossUI()
    {
        bossHpUI.SetActive(false);
    }
}
