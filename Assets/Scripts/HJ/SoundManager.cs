using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   //  사운드 매니저 스크립트
    public static SoundManager sm;
    AudioSource gunSound;


    private void Awake()
    {
        if(sm == null)
        {
            sm = this;
        }
        else
        {
            Destroy(this);
        }
        
    }
    private void Start()
    {
        gunSound = gameObject.transform.Find("GunSound").GetComponent<AudioSource>();
    }

    //  총소리 출력 함수
    public void PlayGunSound()
    {
        gunSound.Stop();
        gunSound.Play();
    }

}
