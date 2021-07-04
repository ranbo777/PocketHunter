using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;
    public List<GameObject> magazine = new List<GameObject>();
    public int magazineValue = 10;
    public GameObject pool;
    public float attackValue = 2.0f;


    private void Start()
    {
        //  �÷��̾��� ���� Ÿ���� �ٰŸ��� ��� ���Ÿ� ���� ���� ��ũ��Ʈ ��Ȱ��ȭ. 
        if (GameManger.playerWeaponType == false)
        {
            gameObject.GetComponent<PlayerFire>().enabled = false;
        }
        //  źȯ �Ҵ�.
        for(int i=0; i<magazineValue; i++)
        {
            magazine.Add(Instantiate(bullet));
            magazine[i].SetActive(false);
            magazine[i].transform.SetParent(pool.transform);
        }
    }
    void Update()
    {

        //  ���� ���콺 ��ư�� ������ �Ѿ� �߻� 
        //  źâ�� ź���� ���� �� ��� �߻�.
        if (Input.GetButtonDown("Fire1"))
        {
            if(magazine.Count > 0)
            {
                for (int i = 0; i<magazineValue; i++)
                {
                    if (magazine[i].activeInHierarchy == false)
                    {
                        magazine[i].SetActive(true);
                        magazine[i].transform.position = transform.position;
                        magazine[i].transform.rotation = transform.rotation;
                        SoundManager.sm.PlayGunSound();
                        break;
                    }
                }
            }
        }                        
    }
}
