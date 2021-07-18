using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public float groggyValue = 1.0f;
    public float attackValue = 2.0f;
    public float bulletDelay = 0.5f;
    public GameObject sup;
    GameManger gm;

    float time = 0;


    void Update()
    {
        time += Time.deltaTime;

        //  ���� ���콺 ��ư�� ������ �Ѿ� �߻� 
        //  źâ�� ź���� ���� �� ��� �߻�.
        if (Input.GetButtonDown("Fire1") && PlayerState.stunCheck == false)
        {
            if (time >= bulletDelay)
            {
                GameObject go = Instantiate(bullet);
                go.transform.position = sup.transform.position;
                SoundManager.sm.PlayGunSound();
                go.transform.rotation = sup.transform.rotation;
                time = 0;
            }

        }
    }
}
