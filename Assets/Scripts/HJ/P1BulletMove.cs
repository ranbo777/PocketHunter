using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1BulletMove : MonoBehaviour
{
    float time = 0;
    public float speed = 0.75f;
    public GameObject trail;
    Vector3 handle1;
    Vector3 handle2;
    Vector3 sPos;
    Vector3 tPos;

    PlayerState pS;

    private void Start()
    {
        pS = GameObject.Find("Player").GetComponent<PlayerState>();
        
    }

    void Update()
    {
        time += Time.deltaTime;

        if(time >= 0.01f) { trail.SetActive(true); }
        
        if(time >= 3.0f) { Destroy(gameObject); }
    }
    private void FixedUpdate()
    {
        BulletMove();
    }

    public void GetHandle(Vector3 startPos, Vector3 targetPos, Vector3 h1, Vector3 h2)
    {
        sPos = startPos;
        tPos = targetPos;
        handle1 = h1;
        handle2 = h2;
    }

    //  탄환 이동 함수
    void BulletMove()
    {
        float move = time * speed;
        Vector3 a = (1 - move) * sPos + move * handle1;
        Vector3 b = (1 - move) * handle1 + move * handle2;
        Vector3 c = (1 - move) * handle2 + move * tPos;
        Vector3 d = (1 - move) * a + move * b;
        Vector3 e = (1 - move) * b + move * c;
        Vector3 Point = (1 - move) * d + move * e;
        transform.position = Point;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if(PlayerState.noHitCheck == false)
            {
                print("맞았다!");
                pS.AddPlayerHP(-15.0f);
                pS.AddStunGauge(1.0f);
                Destroy(gameObject);
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
