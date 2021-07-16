using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public GameObject target;

    Vector3 move;

    float moveSpeed = 200.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {

        transform.RotateAround(target.transform.position, Vector3.down, moveSpeed * Time.deltaTime);
        transform.position += transform.right * 0.5f * Time.deltaTime;
        

    }
}
