using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public int magazineValue = 10;
    public float groggyValue = 1.0f;
    public float attackValue = 2.0f;
    public GameObject sup;

    private void Start()
    {
        //  플레이어의 공격 타입이 근거리일 경우 원거리 공격 관련 스크립트 비활성화. 
        if (GameManger.playerWeaponType == false)
        {
            gameObject.GetComponent<PlayerFire>().enabled = false;
        }
    }
    void Update()
    {

        //  왼쪽 마우스 버튼을 누르면 총알 발사 
        //  탄창에 탄약이 존재 할 경우 발사.
        if (Input.GetButtonDown("Fire1") && PlayerState.stunCheck == false)
        {
            GameObject go = Instantiate(bullet);
            go.transform.position = sup.transform.position;
            SoundManager.sm.PlayGunSound();
            go.transform.rotation = sup.transform.rotation;
        }
    }
}
