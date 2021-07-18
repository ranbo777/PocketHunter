using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
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

    //void Update()
    //{
    //    if (gameObject.activeInHierarchy == true)
    //    {
    //        StartCoroutine(DeleteBullet(existTime));
    //    }        
    //  }

    private void OnTriggerEnter(Collider col)
    {
        //  탄환에 맞은 오브젝트가 보스일 경우 보스에게 데미지를 입힌다.
        if (col.tag.Equals("Boss"))
        {
            bF.TakeMeleeDamage(pf.attackValue, pf.groggyValue);

        }


    }

    //  탄환이 허공에 5초 이상 존재 할 경우 탄환을 삭제하는 함수.
    IEnumerator DeleteBullet(float t)
    {
        yield return new WaitForSeconds(t);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
