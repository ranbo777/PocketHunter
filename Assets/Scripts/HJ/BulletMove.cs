using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed = 0.1f;
    GameObject pa;
    public BossFSM bF;

    private void Start()
    {
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();
        pa = GameObject.Find("Player").transform.Find("Sup").gameObject;
        transform.rotation = pa.transform.rotation;
    }
    void Update()
    {
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
    
    
   
}
