using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public ParticleSystem bullectImpact;
    public ParticleSystem bullectImpact2;
    public ParticleSystem go;
    public ParticleSystem blue;

    public Vector3 attackAnchor;
    PlayerFire pf;
    public BossFSM bF;
    public PlayerEquip PE;

    public enum Type { Melee, Range };
    public Type type;
    int damage;
    float rate;

    
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    bool fDown;
    bool fDown2;
    bool isFireReady;
    float fireDelay;
    GameObject equipWeapon;
    public GameObject pos;

    public Animator anim;

    void start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void GetInput()
    {
        fDown = Input.GetButtonDown("Fire1");
        fDown2 = Input.GetButton("Fire4");


    }
    public void Attack()
    {
        //if (equipWeapon == null)
        //    return;

        //fireDelay += Time.deltaTime;
        //isFireReady = rate < fireDelay;

        if (fDown)
        {


            //use();
            anim.SetTrigger("doSwing");
            // fireDelay = 0;

            Instantiate(go);
            attackAnchor = pos.transform.position;
            go.transform.position = attackAnchor;
            go.Play();

            use();

        }
    }

    public void use()
    {
        StopCoroutine(Shoot());
        StartCoroutine(Shoot());

        //if (type == Type.Melee)
        //{
        IEnumerator Shoot()
        {
            //1
            yield return new WaitForSeconds(0.1f); //0.1 프레임 대기
            meleeArea.enabled = true;
            trailEffect.enabled = true;
            //2
            yield return new WaitForSeconds(0.5f); //0.5 프레임 대기
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.6f); //0.6 프레임 대기
            trailEffect.enabled = false;

        }





    }


    void Update()
    {
        GetInput();
        Attack();


        // 만약 마우스 왼족 버튼을 누르면
        if (fDown2 && fDown)
        {


            //w print("s");
            // 1. 시선을 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 2. 그 시선을 이용해서 바라봤는데 닿은 곳이 있다면?
            RaycastHit rayhit;
            if (Physics.Raycast(ray, out rayhit))
            {// 3. 닿은 곳에 총알 자국을 출력하고 싶다.
                print(rayhit.collider.gameObject.name);


                Instantiate(bullectImpact);
                bullectImpact.transform.position = rayhit.point;



                //5. 총알 자국의 방향을 노멀 방향으로 회전하고 싶다.
                // 총알 자국의 forward 방향과 닿은 곳의 normal 방향을 일치시키고 싶다.
                bullectImpact.transform.forward = rayhit.normal;
            }
        }




    }

}
