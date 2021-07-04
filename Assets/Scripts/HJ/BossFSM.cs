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
        //  보스 hp가 0 밑으로 내려가지 않게 설정.
        HP = Mathf.Max(0, HP);
    }

    public void TakeDamage(float p)
    {
        HP -= p;
    }
}
