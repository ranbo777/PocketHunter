using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    float x;
    float y;
    //float speed = 2.0f;
    //float radius = 2.0f;
    //float time = 0;
    int count = 3;

    public GameObject target;
    public GameObject go1;
    // Start is called before the first frame update
    void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 1; i<=count; i++)
        {            
            //time += Time.deltaTime * speed;
            //x = radius * Mathf.Cos(Random.Range(0, 360));
            //y = radius * Mathf.Sin(Random.Range(0, 360));
            //GameObject ob = Instantiate(go1);
            //ob.transform.position = transform.position +new Vector3(0, x, y);
            print(new Vector3(0, x, y));
            count++;
        }
        
        
        
    }
}
