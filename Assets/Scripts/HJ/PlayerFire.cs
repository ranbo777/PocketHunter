using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public float groggyValue = 1.0f;
    public float attackValue = 2.0f;
    public GameObject sup;
    GameManger gm;

   
    void Update()
    {

        //  ���� ���콺 ��ư�� ������ �Ѿ� �߻� 
        //  źâ�� ź���� ���� �� ��� �߻�.
        if (Input.GetButtonDown("Fire1") && PlayerState.stunCheck == false)
        {
            GameObject go = Instantiate(bullet);
            go.transform.position = sup.transform.position;
            SoundManager.sm.PlayGunSound();
            go.transform.rotation = sup.transform.rotation;
        }
    }
}
