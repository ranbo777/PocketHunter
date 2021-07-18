using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    #region º¸½º ÀÌµ¿ º¯¼ö
    public Vector3 move;
    public float bossSpeed = 5.0f;
    #endregion

    #region º¸½º Ã¼·Â º¯¼ö
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

    #region º¸½º Áß·Â º¯¼ö
    float gravity = -0.8f;
    float yVelocity;
    #endregion

    #region º¸½º °æÁ÷ º¯¼ö
    float bossGroggyTime = 0;
    float bossGroggyValue = 0;
    #endregion

    #region º¸½º ÆÐÅÏ1 º¯¼ö
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

    #region º¸½º ÆÐÅÏ2 º¯¼ö
    public float p2AttackValue = 10.0f;
    bool p2Check = true;
    [HideInInspector] public bool p2ColType = false;
    #endregion

    #region º¸½º ÆÐÅÏ3 º¯¼ö
    bool p3Check = true;
    float p3MoveTime = 0;
    #endregion


    #region º¸½º ÆÐÆ®·Ñ º¯¼ö

    //º¸½º ÆÐÆ®·Ñ º¯¼ö
    float recoveryTime = 0;
    public Transform[] waypoints;
    public int speed;

    private int waypointIndex;
    private float dist;
    public bool BossRest;

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
    

    //  º¸½º FSM
    public enum State
    {
        Idle,
        Move,
        Patrol,
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
        BossRest = true;

        //ÆÐÆ®·Ñ
        waypointIndex = 0;
        LookPatrolTarget();

    }

    void Update()
    {

        yVelocity += gravity * Time.deltaTime;

        //cc.Move(new Vector3(0, yVelocity, 0));

        distance = (target.transform.position - transform.position).magnitude;

        //print(distance);

        time += Time.deltaTime;
        time2 += Time.deltaTime;
        BossHPCheck();



        //if (distance <= bossAttackDistance && bossState != State.Attack)
        //{
        //    print("º¸½º »óÅÂ ÀüÈ¯ Attack");
        //    bossState = State.Attack;
        //}
        //else if (distance > bossAttackDistance && bossState != State.Idle)
        //{
        //    print("º¸½º »óÅÂ ÀüÈ¯ Idle");
        //    bossState = State.Idle;
        //}

        if (bossGroggyValue >= 10.0f && bossState != State.Pattern_3)
        {
            bossState = State.Groggy;
            am.SetBool("OnGroggy", true);
            print("º¸½º °æÁ÷");
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


                if (BossRest != false)
                {
                    bossState = State.Patrol;
                    print("º¸½º Á¤Âû ½ÇÇà");
                    am.SetFloat("Runspeed", move.magnitude);
                    return;
                }
                else 
                { 

                {print("º¸½º Á¤Âû ¾ÈÇÔ");
                    //
                    bossState = State.Move;
                    if (distance <= 2.0f)
                    {
                        p2Check = true;
                        bossState = State.Pattern_2;
                        print("ÆÐÅÏ2 ½ÇÇà");
                    }

                    if (distance > 2.0f && distance <= 8.0f)
                    {
                        int p = Random.Range(0, 100);
                        if (p >= 90)
                        {
                            p1Check = true;
                            bossState = State.Pattern_1;
                            print("ÆÐÅÏ1 ½ÇÇà");
                        }
                        else { bossState = State.Move; }
                    }

<<<<<<< HEAD
                        if (distance > 2.0f && distance <= 8.0f)
<<<<<<< HEAD
=======
=======
>>>>>>> parent of a36ec35 (0718 _JY)
                    if (distance > 8.0f)
                    {
                        int p = Random.Range(0, 100);

<<<<<<< HEAD
                        if (p <= 20)
<<<<<<< HEAD
>>>>>>> parent of c35e3a0 (ê¸°íƒ€ ìˆ˜ì •)
=======
>>>>>>> parent of c35e3a0 (ê¸°íƒ€ ìˆ˜ì •)
=======
>>>>>>> parent of 5c03c1e (HJ.Revert)
=======
                        if (p <= 50)
>>>>>>> parent of a36ec35 (0718 _JY)
                        {
                            p1Check = true;
                            bossState = State.Pattern_1;
                            print("ÆÐÅÏ1 ½ÇÇà");
                        }
                        else
                        {
                            p3Check = true;
                            p3MoveTime = 0;
                            bossState = State.Pattern_3;
                            print("ÆÐÅÏ3 ½ÇÇà");
                        }

                    }
                }

                }
                //if (Input.GetKeyDown(KeyCode.O))
                //{
                //    p1Check = true;
                //    bossState = State.Pattern_1;
                //    print("ÆÐÅÏ1 ½ÇÇà");
                //}
                //if (Input.GetKeyDown(KeyCode.P))
                //{
                //    p2Check = true;
                //    bossState = State.Pattern_2;
                //    print("ÆÐÅÏ2 ½ÇÇà");
                //}
                break;
            case State.Move:
                LookTarget();
                print("³ÊÀÌ¸®¿Í");
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
                    print("º¸½º °æÁ÷ ÇØÁ¦");
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

            case State.Patrol:
                // ÆÐÆ®·Ñ
                //LookPatrolTarget();
                
                transform.LookAt(waypoints[waypointIndex].transform.position);
                print("µµÁÖ");

                Transform RPoint = waypoints[waypointIndex];
                Transform BPoint = gameObject.transform;
                move = RPoint.position - BPoint.position;
                move.Normalize();
                dist = Vector3.Distance(BPoint.position, RPoint.position);
                BossRecover();
                if (dist < 5f)
                {
                    IncreaseIndex();

                }
                Patrol();
                break;
        }        
        //  º¸½º hp°¡ 0 ¹ØÀ¸·Î ³»·Á°¡Áö ¾Ê°Ô ¼³Á¤.
        HP = Mathf.Max(0, HP);
    }

    //  º¸½º°¡ ¿ÜºÎ¿¡¼­ µ¥¹ÌÁö¸¦ ¹Þ´Â ÇÔ¼ö.
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

    void LookPatrolTarget()
    {
        
        transform.LookAt(waypoints[waypointIndex].position);

    }

    //  ÆÐÅÏ1: º¸½º°¡ count ¸¸Å­ÀÇ Åõ»çÃ¼¸¦ »ý¼ºÇØ Àû¿¡°Ô ¹ß»ç.
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

    //  ÆÐÅÏ2: º¸½º°¡ ±ÙÁ¢ °ø°ÝÀ» ÇÔ
    void Pattern_2()
    {
        am.SetBool("OnAttack1", true);
        p2ColType = true;
    }

    void Pattern_3()
    {
        am.SetBool("OnJumpAttack", true);
    }

    //  º¸½º°¡ ÆÐÅÏ »ç¿ë ÈÄ Idle »óÅÂ·Î ³Ñ¾î°¥ ¶§ ±îÁö °É¸®´Â µô·¹ÀÌ ÇÔ¼ö
    void ReturnState()
    {
        bossState = State.Idle;
        print("ÆÐÅÏ µô·¹ÀÌ Á¾·á");
    }

    //º¸½º Á×À½
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


    void Patrol()
    {
        //print("¿òÁ÷ÀÓ");
        cc.Move(move * bossSpeed * Time.deltaTime);
        am.SetFloat("Runspeed", move.magnitude);

    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {

            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }

    void BossHPCheck()
    {
        if (80 < HP && HP < 150)
        {
            BossRest = false;
        }
        else 
        {
            BossRest = true;
           
        }
        

    }

    void BossRecover()
    {
        recoveryTime += Time.deltaTime;
        if (recoveryTime >= 3.0f && HP < 100.0f)
        {
            HP += Time.deltaTime * 5.0f;
            HP = Mathf.Min(100, HP);
        }

    }
}
