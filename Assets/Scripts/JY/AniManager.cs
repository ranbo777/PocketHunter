using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniManager : MonoBehaviour
{
    public Animation myAnim;
    public AnimationClip[] myClips = new AnimationClip[6];

    // 애니메이션 클립
    
        
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponentInChildren<Animation>();
        
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
