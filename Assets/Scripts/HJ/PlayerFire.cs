using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public float groggyValue = 0.5f;
    public float attackValue = 2.0f;
    public float bulletDelay = 0.75f;

    public float burstDelay;
    public float burstMinDelay = 0.05f;
    public GameObject sup;
    GameManger gm;

    public GameObject particle1;
    GameObject pc1;


    float keepClickTime;


    bool burstModeCheck = false;
    public static bool burstModeSubCheck = false;

    float time = 0;

    void Start()
    {
        pc1 = Instantiate(particle1, transform.position, Quaternion.identity);
        pc1.SetActive(false);
    }

    void Update()
    {
        time += Time.deltaTime;
        keepClickTime -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            keepClickTime = 0.05f;
        }

        //  왼쪽 마우스 버튼을 누르면 총알 발사 
        //  탄창에 탄약이 존재 할 경우 발사.
        if (keepClickTime > 0 && PlayerState.stunCheck == false)
        {
            if (time >= bulletDelay && burstModeSubCheck == false)
            {
                GameObject go = Instantiate(bullet);
                go.transform.position = sup.transform.position;
                SoundManager.sm.PlayGunSound();
                go.transform.rotation = sup.transform.rotation;
                time = 0;
            }

            if (time >= burstDelay && burstModeCheck == true)
            {
                GameObject go = Instantiate(bullet);
                go.transform.position = sup.transform.position;
                SoundManager.sm.PlayGunSound();
                go.transform.rotation = sup.transform.rotation;
                time = 0;
                burstDelay -= 0.1f;
                burstDelay = Mathf.Max(burstMinDelay, burstDelay);
            }

        }
        // 앉아쏘기 구현
        // 모션이 실행되면 자동으로 줌 상태가 된다.
        // 모션이 실행되는 동안 움직일 수 없다.
        // 모션이 실행되면 발사 딜레이가 점점 짦아진다.
        // 구르기 행동으로 모션을 캔슬 시킨다.
        if (Input.GetButtonDown("Burst") && PlayerState.stunCheck == false && burstModeSubCheck == false)
        {
            print("앉아쏘기 실행");
            PlayerState.playerZoomCheck = false;
            Invoke("BurstMode", 0.5f);
            burstModeSubCheck = true;
            pc1.transform.position = transform.position;
            pc1.SetActive(true);
        }

        if (burstModeSubCheck == false)
        {
            burstModeCheck = false;
            pc1.SetActive(false);
        }
    }

    void BurstMode()
    {
        burstModeCheck = true;
        PlayerState.playerZoomCheck = true;
        burstDelay = bulletDelay;
    }
}
