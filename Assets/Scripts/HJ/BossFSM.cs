using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    #region ���� �̵� ����
    public Vector3 move;
    public float bossSpeed = 4.0f;
    #endregion

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

    #region ���� �߷� ����
    float gravity = -0.8f;
    float yVelocity;
    #endregion

    #region ���� ���� ����
    float bossGroggyTime = 0;
    float bossGroggyValue = 0;
    #endregion

    #region ���� ����1 ����
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

    #region ���� ����2 ����
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

    //  ���� FSM
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
        //    print("���� ���� ��ȯ Attack");
        //    bossState = State.Attack;
        //}
        //else if (distance > bossAttackDistance && bossState != State.Idle)
        //{
        //    print("���� ���� ��ȯ Idle");
        //    bossState = State.Idle;
        //}

        if (bossGroggyValue >= 10.0f)
        {
            bossState = State.Groggy;
            print("���� ����");
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
                    print("����1 ����");
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    p2Check = true;
                    bossState = State.Pattern_2;
                    print("����2 ����");
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
                    print("���� ���� ����");
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
        //  ���� hp�� 0 ������ �������� �ʰ� ����.
        HP = Mathf.Max(0, HP);
    }

    private void BossAttack(float value, float stunValue)
    {
        print("�����ؾ���");
        targetState.AddPlayerHP(-value);
        targetState.AddStunGauge(stunValue);
    }

    //  ������ �ܺο��� �������� �޴� �Լ�.
    public void TakeDamage(float playerAttackValue, float playerGroggyValue)
    {
        HP -= playerAttackValue;
        bossGroggyValue += playerGroggyValue;
    }

    void LookTarget()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        
    }

    //  ����1: ������ count ��ŭ�� ����ü�� ������ ������ �߻�.
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

    //  ����2: ������ ���� ������ ��
    void Pattern_2()
    {
        am.SetBool("OnAttack1", true);
        p2ColType = true;
    }

    //  ������ ���� ��� �� Idle ���·� �Ѿ �� ���� �ɸ��� ������ �Լ�
    void RetrunState()
    {
        bossState = State.Idle;
        print("���� ������ ����");
    }
}
