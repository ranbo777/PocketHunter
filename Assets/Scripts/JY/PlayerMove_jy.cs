using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_jy : MonoBehaviour
{
    // ������� �Է¿� ���� �յ� �¿�� �̵��Ѵ�.
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
        
        // 1. y�ӵ��� �߷��� ��� ���ϰ�ʹ�.
        yVelocity += gravity * Time.deltaTime;
        // 2. ���� ����ڰ� ���� ��ư�� ������ y �ӵ��� �ٴ� ���� �����ϰ� �ʹ�.
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
       




        // 1. ������� �Է¿�����

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // 2. �յ��¿� ������ �����

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        //ī�޶� �ٶ󺸴� ������ �չ������� �ϰ�ʹ�.
        dir = Camera.main.transform.TransformDirection(dir);

        // dir ũ�⸦ 1�� ����� �ʹ�.

        dir.Normalize();

        // 3. y�ӵ��� ���� dir�� y�� �����ϰ�ʹ�.

        dir.y = yVelocity;
        // 3. y�ӵ��� ���� dir�� y�� �����ϰ�ʹ�.
        //  3. �� �������� �̵��Ѵ�. 

        // p = p0 + vt

        cc.Move(dir * speed * Time.deltaTime);


    }
}
