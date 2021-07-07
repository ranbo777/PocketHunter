using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   //  ���� �Ŵ��� ��ũ��Ʈ
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

    //  �ѼҸ� ��� �Լ�
    public void PlayGunSound()
    {
        gunSound.Stop();
        gunSound.Play();
    }

}
