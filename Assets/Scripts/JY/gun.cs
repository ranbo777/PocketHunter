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
        // ���� ���콺 ���� ��ư�� ������
        if (Input.GetButtonDown("Fire1"))
        {

     

        // 1. �ü��� �����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // 2. �� �ü��� �̿��ؼ� �ٶ�ôµ� ���� ���� �ִٸ�?
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {// 3. ���� ���� �Ѿ� �ڱ��� ����ϰ� �ʹ�.
                
                
               bullectImpact.transform.position = hitinfo.point;

                bF = GameObject.Find("Boss").GetComponent<BossFSM>();

                //4. �Ѿ��ڱ� vfx ����ϰ� �ʹ�. 

                //print(hitinfo.transform.name);

            bullectImpact.Stop();
            bullectImpact.Play();

                //5. �Ѿ� �ڱ��� ������ ��� �������� ȸ���ϰ� �ʹ�.
                // �Ѿ� �ڱ��� forward ����� ���� ���� normal ������ ��ġ��Ű�� �ʹ�.
                bullectImpact.transform.forward = hitinfo.normal;
        }
        }

    }
   
}
