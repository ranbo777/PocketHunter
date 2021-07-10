using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed = 0.1f;
    PlayerFire pf;
    public BossFSM bF;
    public float existTime = 3.0f;
    Rigidbody rb;

    //bool check = false;
    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();
        pf = GameObject.Find("Player").GetComponent<PlayerFire>();
        
    }

    void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            StartCoroutine(DeleteBullet(existTime));
        }        
    }

    private void FixedUpdate()
    {
        //  �Ѿ� �̵�                
        //  transform.position += transform.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);
        rb.velocity += transform.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);


    }

    void OnTriggerEnter(Collider col)
    {
        //  źȯ�� ���� ������Ʈ�� ������ ��� �������� �������� ������.
        if (col.tag.Equals("Boss"))
        {
            bF.TakeDamage(pf.attackValue);
            Destroy(gameObject);            
        }
        if (col.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }
    
    //  źȯ�� ����� 5�� �̻� ���� �� ��� źȯ�� �����ϴ� �Լ�.
    IEnumerator DeleteBullet(float t)
    {
        yield return new WaitForSeconds(t);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
   
}
