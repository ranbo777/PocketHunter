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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

    public PlayerState ps;
    
    public BoxCollider meleeArea;
=======
    float rate;
    public SphereCollider meleeArea;
>>>>>>> parent of bd445c9 (수정)
=======
    float rate;
    public SphereCollider meleeArea;
>>>>>>> parent of bd445c9 (수정)
=======
    float rate;
    public SphereCollider meleeArea;
>>>>>>> parent of bd445c9 (수정)
    public TrailRenderer trailEffect;
    bool fDown;
    bool fDown2;
    public bool isFireReady;
    public float fireDelay;
    GameObject equipWeapon;
    public float rate;
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

    void Update()
    {
        GetInput();
        Attack();


        fireDelay += Time.deltaTime;
        if (rate < fireDelay)
        {
            isFireReady = true;

            fireDelay = 0;
        };
        // ���� ���콺 ���� ��ư�� ������
        if (fDown2 && fDown)
        {


            //w print("s");
            // 1. �ü��� �����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 2. �� �ü��� �̿��ؼ� �ٶ�ôµ� ���� ���� �ִٸ�?
            RaycastHit rayhit;
            if (Physics.Raycast(ray, out rayhit))
            {// 3. ���� ���� �Ѿ� �ڱ��� ����ϰ� �ʹ�.
                print(rayhit.collider.gameObject.name);


                Instantiate(bullectImpact);
                bullectImpact.transform.position = rayhit.point;



                //5. �Ѿ� �ڱ��� ������ ��� �������� ȸ���ϰ� �ʹ�.
                // �Ѿ� �ڱ��� forward ����� ���� ���� normal ������ ��ġ��Ű�� �ʹ�.
                bullectImpact.transform.forward = rayhit.normal;
            }
        }

        if (fDown && isFireReady == true && PlayerState.stunCheck != true )
        {



            anim.SetTrigger("doSwing");
            fireDelay = 0;

            Instantiate(go);
            attackAnchor = pos.transform.position;
            go.transform.position = attackAnchor;
            go.Play();

            use();
            isFireReady = false;
        }


    }

    public void Attack()
    {
        if (equipWeapon == null)
        { return; }
    }

    public void use()
    {
        StopCoroutine(Shoot());
        StartCoroutine(Shoot());

        //if (type == Type.Melee)
        //{
        IEnumerator Shoot()
        {
            isFireReady = false;
            //1
            yield return new WaitForSeconds(0.1f); //1 ������ ���
            meleeArea.enabled = true;
            trailEffect.enabled = true;
            //2
            yield return new WaitForSeconds(0.5f); //1 ������ ���
            meleeArea.enabled = false;

            yield return new WaitForSeconds(0.6f); //1 ������ ���
            trailEffect.enabled = false;

            yield return new WaitForSeconds(3f);
            
        }


    }

   
    

}
