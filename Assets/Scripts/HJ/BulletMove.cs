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

        transform.position += transform.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);
                         
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Boss"))
        {
            bF.TakeDamage(pa.GetComponent<PlayerFire>().attackValue);
            gameObject.SetActive(false);
            
        }
    }
    
    IEnumerator DeleteBullet(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
   
}
