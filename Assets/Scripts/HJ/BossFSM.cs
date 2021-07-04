using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public float maxBossHP = 10.0f;
    float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }
 
    // Start is called before the first frame update
    void Start()
    {
        HP = maxBossHP;
    }

    // Update is called once per frame
    void Update()
    {
        //  ���� hp�� 0 ������ �������� �ʰ� ����.
        HP = Mathf.Max(0, HP);
    }

    public void TakeDamage(float p)
    {
        HP -= p;
    }
}
