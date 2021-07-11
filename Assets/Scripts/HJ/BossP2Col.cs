using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP2Col : MonoBehaviour
{
    //  ����1 �ݶ��̴�, ����Ʈ ����
    public GameObject Lhand;
    public GameObject LhandEffect;
    public GameObject Rhand;
    public GameObject RhandEffect;

    //  ������Ʈ Ȱ��ȭ �Լ�
    public void OnPattern2Col()
    {
        Lhand.SetActive(true);
        LhandEffect.SetActive(true);
        Rhand.SetActive(true);
        RhandEffect.SetActive(true);
    }

    //  ������Ʈ ��Ȱ��ȭ �Լ�
    public void OffPattern2Col()
    {
        Lhand.SetActive(false);
        LhandEffect.SetActive(false);
        Rhand.SetActive(false);
        RhandEffect.SetActive(false);
    }
}
