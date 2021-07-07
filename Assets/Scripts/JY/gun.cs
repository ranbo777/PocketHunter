using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public BossFSM bF;
    public ParticleSystem bullectImpact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 만약 마우스 왼족 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {

     

        // 1. 시선을 만들고
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // 2. 그 시선을 이용해서 바라봤는데 닿은 곳이 있다면?
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {// 3. 닿은 곳에 총알 자국을 출력하고 싶다.
                
                
               bullectImpact.transform.position = hitinfo.point;

                bF = GameObject.Find("Boss").GetComponent<BossFSM>();

                //4. 총알자국 vfx 재생하고 싶다. 

                //print(hitinfo.transform.name);

            bullectImpact.Stop();
            bullectImpact.Play();

                //5. 총알 자국의 방향을 노멀 방향으로 회전하고 싶다.
                // 총알 자국의 forward 방향과 닿은 곳의 normal 방향을 일치시키고 싶다.
                bullectImpact.transform.forward = hitinfo.normal;
        }
        }

    }
   
}
