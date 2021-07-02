using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public float bossHp = 10.0f;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  보스 hp가 0 밑으로 내려가지 않게 설정.
        bossHp = Mathf.Max(0, bossHp);
    }

    public void TakeDamage(float p)
    {
        bossHp -= p;
    }
}
