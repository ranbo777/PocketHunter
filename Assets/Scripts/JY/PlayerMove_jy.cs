using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_jy : MonoBehaviour
{
    // 사용자의 입력에 따라 앞뒤 좌우로 이동한다.
    public float speed = 10;
    public float gravity = -9.81f;
    public float jumpPower = 10;
    float yVelocity;

    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // 1. y속도에 중력을 계속 더하고싶다.
        yVelocity += gravity * Time.deltaTime;
        // 2. 만약 사용자가 점프 버튼을 누르면 y 속도에 뛰는 힘을 대입하고 싶다.
        if(Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20;

        }
        else
        {
            speed = 10;

        }
       




        // 1. 사용자의 입력에따라

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // 2. 앞뒤좌우 방향을 만들고

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        //카메라가 바라보는 방향을 앞방향으로 하고싶다.
        dir = Camera.main.transform.TransformDirection(dir);

        // dir 크기를 1로 만들고 싶다.

        dir.Normalize();

        // 3. y속도를 최종 dir의 y에 대입하고싶다.

        dir.y = yVelocity;
        // 3. y속도를 최종 dir의 y에 대입하고싶다.
        //  3. 그 방향으로 이동한다. 

        // p = p0 + vt

        cc.Move(dir * speed * Time.deltaTime);


    }
}
