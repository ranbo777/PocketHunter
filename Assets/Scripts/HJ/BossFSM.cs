using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    #region 보스 이동 변수
    public Vector3 move;
    public float bossSpeed = 4.0f;
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

    float distance;

    GameObject target;
    PlayerState targetState;
    CharacterController cc;
    Animator am;

    float time;

    //  보스 FSM
    public enum State
    {
        Idle,
        Move,
        Groggy,
        Pattern_1,
        Pattern_2,
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

        cc.Move(new Vector3(0, yVelocity, 0));

        distance = (target.transform.position - transform.position).magnitude;

        time += Time.deltaTime;

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

        if (bossGroggyValue >= 10.0f)
        {
            bossState = State.Groggy;
            print("보스 경직");
        }
        
        if(HP <= 0)
        {
            bossState = State.Dead;
        }

        switch(bossState)
        {
            case State.Idle:
                //LookTarget();
                if (Input.GetKeyDown(KeyCode.O))
                {
                    p1Check = true;
                    bossState = State.Pattern_1;
                    print("패턴1 실행");
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    p2Check = true;
                    bossState = State.Pattern_2;
                    print("패턴2 실행");
                }
                break;
            case State.Move:
                LookTarget();
                move = new Vector3(target.transform.position.x - transform.position.x, 0, 
                    target.transform.position.z - transform.position.z);
                move.Normalize();
                am.SetFloat("Runspeed", move.magnitude);
                cc.Move(move * bossSpeed * Time.deltaTime);
                break;
            case State.Groggy:
                bossGroggyTime += Time.deltaTime;
                am.SetBool("OnGroggy", true);
                if(bossGroggyTime >= 3.0f)
                {
                    bossGroggyValue = 0;
                    bossGroggyTime = 0;
                    bossState = State.Idle;
                    am.SetBool("OnGroggy", false);
                    am.SetBool("OnIdle", true);
                    print("보스 경직 해제");
                }
                break;
            case State.Pattern_1:
                if(p1Check == true) 
                { 
                    StartCoroutine(Pattern_1(15)); 
                    p1Check = false;
                    Invoke("RetrunState", 2.5f);
                }                
                break;
            case State.Pattern_2:
                if(p2Check == true)
                {
                    Pattern_2();
                    p2Check = false;
                    Invoke("RetrunState", 1.5f);                    
                }
                break;
            case State.Dead:
                    Destroy(gameObject);
                break;
        }        
        //  보스 hp가 0 밑으로 내려가지 않게 설정.
        HP = Mathf.Max(0, HP);
    }

    private void BossAttack(float value, float stunValue)
    {
        print("공격해쓰요");
        targetState.AddPlayerHP(-value);
        targetState.AddStunGauge(stunValue);
    }

    //  보스가 외부에서 데미지를 받는 함수.
    public void TakeDamage(float playerAttackValue, float playerGroggyValue)
    {
        HP -= playerAttackValue;
        bossGroggyValue += playerGroggyValue;
    }

    void LookTarget()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        
    }

    //  패턴1: 보스가 count 만큼의 투사체를 생성해 적에게 발사.
    IEnumerator Pattern_1(int count)
    {
        int count_ = count;
        while (count_ > 0)
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

    //  보스가 패턴 사용 후 Idle 상태로 넘어갈 때 까지 걸리는 딜레이 함수
    void RetrunState()
    {
        bossState = State.Idle;
        print("패턴 딜레이 종료");
    }
}
