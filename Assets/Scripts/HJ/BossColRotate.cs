using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossColRotate : MonoBehaviour
{
    public BossFSM bF;
    public Animation am;
    public AnimationClip ac1;
    public AnimationClip ac2;
    bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        if(bF == null) { bF = gameObject.transform.parent.GetComponent<BossFSM>(); }
        if(am == null) { am = gameObject.GetComponent<Animation>(); }
    }

    // Update is called once per frame
    void Update()
    {
       if( bF.bossState == BossFSM.State.Groggy)
        {
            if(check == false)
            {
                print("½ÇÇà");
                am.clip = ac1;
                am.Play();
                check = true;
            }            
        }
       if(bF.bossState != BossFSM.State.Groggy)
        {
            if(check == true)
            {
                am.clip = ac2;
                am.Play();
                check = false;
            }
            
        }
       

    }
}
