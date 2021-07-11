using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP2Col : MonoBehaviour
{
    //  패턴1 콜라이더, 이펙트 변수
    public GameObject Lhand;
    public GameObject LhandEffect;
    public GameObject Rhand;
    public GameObject RhandEffect;

    //  오브젝트 활성화 함수
    public void OnPattern2Col()
    {
        Lhand.SetActive(true);
        LhandEffect.SetActive(true);
        Rhand.SetActive(true);
        RhandEffect.SetActive(true);
    }

    //  오브젝트 비활성화 함수
    public void OffPattern2Col()
    {
        Lhand.SetActive(false);
        LhandEffect.SetActive(false);
        Rhand.SetActive(false);
        RhandEffect.SetActive(false);
    }
}
