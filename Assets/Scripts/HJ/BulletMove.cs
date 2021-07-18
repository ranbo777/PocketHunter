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
    public GameObject particle1;
    GameObject pc1;

    //bool check = false;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (GameObject.Find("Boss") != null) { bF = GameObject.Find("Boss").GetComponent<BossFSM>(); }
        pf = GameObject.Find("Player").GetComponent<PlayerFire>();
        pc1 = Instantiate(particle1);
        pc1.SetActive(false);
        StartCoroutine(DeleteBullet(existTime));
    }

    private void FixedUpdate()
    {
        //  총알 이동                
        //  transform.position += transform.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);
        rb.velocity += transform.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);


    }

    void OnTriggerEnter(Collider col)
    {
        //  탄환에 맞은 오브젝트가 보스일 경우 보스에게 데미지를 입힌다.
        if (col.tag.Equals("Boss"))
        {
            pc1.transform.position = transform.position;
            pc1.SetActive(true);
            bF.TakeDamage(pf.attackValue, pf.groggyValue);
            CameraMove.shakeTime = 0.15f;
            Destroy(gameObject);
            Destroy(pc1, 0.5f);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Ground"))
        {
            Destroy(gameObject);
            Destroy(pc1);
        }
    }

    //  탄환이 허공에 5초 이상 존재 할 경우 탄환을 삭제하는 함수.
    IEnumerator DeleteBullet(float t)
    {
        yield return new WaitForSeconds(t);
        //gameObject.SetActive(false);
        Destroy(gameObject);
        Destroy(pc1);
    }

}
