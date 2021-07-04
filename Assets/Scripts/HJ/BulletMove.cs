using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed = 0.1f;
    GameObject pa;
    public BossFSM bF;
    public float existTime = 5.0f;
    
    

    private void Start()
    {
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();
        pa = GameObject.Find("Player").transform.Find("Sup").gameObject;
        
    }

    void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            StartCoroutine(DeleteBullet(existTime));
        }

        //  총알 이동
        transform.position += transform.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);
                         
    }

    void OnTriggerEnter(Collider col)
    {
        //  탄환에 맞은 오브젝트가 보스일 경우 보스에게 데미지를 입힌다.
        if (col.tag.Equals("Boss"))
        {
            bF.TakeDamage(pa.GetComponent<PlayerFire>().attackValue);
            gameObject.SetActive(false);
            
        }
    }
    
    //  탄환이 허공에 5초 이상 존재 할 경우 탄환을 삭제하는 함수.
    IEnumerator DeleteBullet(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
   
}
