using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed = 0.1f;
    Vector3 move;
    GameObject pa;

    private void Start()
    {
        pa=GameObject.Find("Sup");
        transform.rotation = pa.transform.rotation;
    }
    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);
                
         
    }
}
