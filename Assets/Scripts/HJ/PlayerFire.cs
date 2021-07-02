using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public List<GameObject> magazine = new List<GameObject>();
    public int magazineValue = 10;
    public GameObject pool;


    private void Start()
    {
        for(int i=0; i<magazineValue; i++)
        {
            magazine.Add(Instantiate(bullet));
            magazine[i].SetActive(false);
            magazine[i].transform.SetParent(pool.transform);
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(magazine.Count > 0)
            {
                for (int i = 0; i<magazineValue; i++)
                {
                    if (magazine[i].activeInHierarchy == false)
                    {
                        magazine[i].SetActive(true);
                        magazine[i].transform.position = transform.GetChild(0).transform.position;
                        break;
                    }
                }
            }
        }
                
    }
}
