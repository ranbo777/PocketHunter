using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public List<GameObject> magazine = new List<GameObject>();
    public int magazineValue = 10;
    public GameObject pool;
    public float attackValue = 2.0f;


    private void Start()
    {
        //  플레이어의 공격 타입이 근거리일 경우 원거리 공격 관련 스크립트 비활성화. 
        if (GameManger.playerWeaponType == false)
        {
            gameObject.GetComponent<PlayerFire>().enabled = false;
        }
        //  탄환 할당.
        for(int i=0; i<magazineValue; i++)
        {
            magazine.Add(Instantiate(bullet));
            magazine[i].SetActive(false);
            magazine[i].transform.SetParent(pool.transform);
        }
    }
    void Update()
    {

        //  왼쪽 마우스 버튼을 누르면 총알 발사 
        //  탄창에 탄약이 존재 할 경우 발사.
        if (Input.GetButtonDown("Fire1"))
        {
            if(magazine.Count > 0)
            {
                for (int i = 0; i<magazineValue; i++)
                {
                    if (magazine[i].activeInHierarchy == false)
                    {
                        magazine[i].SetActive(true);
                        magazine[i].transform.position = transform.position;
                        magazine[i].transform.rotation = transform.rotation;
                        SoundManager.sm.PlayGunSound();
                        break;
                    }
                }
            }
        }                        
    }
}
