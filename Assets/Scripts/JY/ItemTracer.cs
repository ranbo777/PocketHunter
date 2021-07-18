using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTracer : MonoBehaviour
{

    public Vector3 move;
    public GameObject BPoint;
    public int bugSpeed = 5;
    BossFSM bF;

    void Start()
    {
        BPoint = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (BPoint != null)
        { use(); }
       
    }
    public void use()
    {
        Transform MPoint = gameObject.transform;
        StopCoroutine(go());
        StartCoroutine(go());
        

        //if (type == Type.Melee)
        //{
        IEnumerator go()
        {
            TraceBoss();
            //1
            yield return new WaitForSeconds(0.3f); //0.1 프레임 대기
            move = new Vector3 (0,0.7f,0);
            //2
            yield return new WaitForSeconds(0.7f); //0.5 프레임 대기
            move = BPoint.transform.position - MPoint.position;
            move.Normalize();
            TraceBoss();

            yield return new WaitForSeconds(3f); //0.6 프레임 대기
            Destroy(gameObject);

        }

    }
    void TraceBoss() 
    {
        transform.Translate ( move * bugSpeed * Time.deltaTime);
    }
   

      
}
