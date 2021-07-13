using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerMove pm;
    public Animation am;
    public AnimationClip am1;
    public AnimationClip am2;
    void Start()
    {
        if(pm == null) { gameObject.transform.parent.GetComponent<PlayerMove>(); }
        if(am == null) { am = gameObject.GetComponent<Animation>(); }
    }

    // Update is called once per frame
    void Update()
    {
        //if(pm.check == true)
        //{
        //    pm.anim.SetTrigger("doDodge");
        //}
        //if(PlayerState.stunCheck == true)
        //{
        //    pm.anim.SetTrigger("doStun");
        //}
        //else if (PlayerState.stunCheck == false)
        //{
        //    am.Stop("P_StunHJ");
        //}

        if (pm.check == true)
        {
            am.clip = am1;
            am.Play();
        }
        if (PlayerState.stunCheck == true)
        {
            am.clip = am2;
            am.Play();
        }
        else if (PlayerState.stunCheck == false)
        {
            am.Stop("P_StunHJ");
        }

    }
}
