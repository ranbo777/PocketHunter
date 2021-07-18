using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public float groggyValue = 1.0f;
    public float attackValue = 2.0f;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    public float bulletDelay = 0.5f;
=======
    public float bulletDelay = 0.75f;

    public float burstDelay;
    public float burstMinDelay = 0.05f;
>>>>>>> parent of c35e3a0 (ê¸°íƒ€ ìˆ˜ì •)
=======
    public float bulletDelay = 0.5f;
>>>>>>> parent of bd445c9 (ìˆ˜ì •)
=======
    public float bulletDelay = 0.5f;
>>>>>>> parent of bd445c9 (ìˆ˜ì •)
=======
    public float bulletDelay = 0.5f;
>>>>>>> parent of bd445c9 (ìˆ˜ì •)
    public GameObject sup;
    GameManger gm;

    float time = 0;


    void Update()
    {
        time += Time.deltaTime;

        //  ¿ÞÂÊ ¸¶¿ì½º ¹öÆ°À» ´©¸£¸é ÃÑ¾Ë ¹ß»ç 
        //  ÅºÃ¢¿¡ Åº¾àÀÌ Á¸Àç ÇÒ °æ¿ì ¹ß»ç.
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
