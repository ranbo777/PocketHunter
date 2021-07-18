using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{

    bool iEquip;
    public GameObject[] weapons;
    public bool[] hasEquip;
    public GameManger gm;
    public PlayerMove pm;
    public GameObject equipWeapon;
    public PlayerFire pF;
    public bool isSwap;

    PlayerAnimation pa;
    public Melee melee;



    public bool sDown1;
    public bool sDown2;


    private void Start()
    {
        pF.enabled = false;
        melee.enabled = false;
        gm.gun.SetActive(false);
    }
    // Update is called once per frame
    public void Update()
    {
        GetInput();
        swap();
    }

    public void GetInput()
    {
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");

    }



    //Vector3 dir = go.transform.position;
    // attackAnchor = dir;



    void swap()
    {
        int weaponIndex = -1;

        if (sDown1)
        {

            weaponIndex = 0;
            gm.Melee = false;
            gm.gun.SetActive(true);
            gm.sword.SetActive(false);
            if (gm.Melee == false)
            {
                melee.enabled = false;
                pF.enabled = true;

            }
        }
        if (sDown2)
        {
            weaponIndex = 1;
            gm.Melee = true;
            gm.gun.SetActive(false);
            gm.sword.SetActive(true);
            if (gm.Melee == true)
            {
                pF.enabled = false;
                melee.enabled = true;
            }
        }

        if ((sDown1 || sDown2) && !pm.isDodge)
        {
            if (equipWeapon != null)
            {
                equipWeapon.SetActive(false);
            }
            equipWeapon = weapons[weaponIndex];

            equipWeapon.SetActive(true);



            isSwap = true;
            Invoke("SwapOut", 0.4f);
        }

        //void SwapOut()
        //{
        //    isSwap = false;
        //}


    }
    void SwapOut()
    {

        isSwap = false;

    }
}
