using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{

    public GameObject target;
    Vector3 targetPos;

    private void Start()
    {

    }

    void Update()
    {

        targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        transform.LookAt(targetPos);
        
        

    }
}
