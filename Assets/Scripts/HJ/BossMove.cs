using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{

    GameObject target;
    Vector3 targetPos;

    private void Start()
    {
        target = GameObject.Find("Player");
    }

    void Update()
    {

        targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        transform.LookAt(targetPos);
        
        

    }
}
