using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{


    public GameObject menuCam;
    public GameObject gameCam;
    public GameObject player;
    //public Boss boss;
    public string bossname = "pikachu";

    
    public float playTime = 0;
    public bool isBattle = true;
    public int enemyCntA;
    public int enemyCntB;
    public int enemyCntC;

    public GameObject menuPanel;
    public GameObject gamePanel;

    public Text maxTimeTxt;
    public Text BossStageTxt;
    public Text playTimeTxt;
    public Text playTime2Txt;

    public Text playerHealthTxt;
    public RectTransform playerHealthBar;
    public Text playerStaminaTxt;
    public Text playerAmmoTxt;
    public Text playerCoinTxt;

    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image weapon4Img;
    public Image weaponRImg;

    public RectTransform BossHealthGroup;
    public RectTransform BossHealthBar;

    public Image crosshair;

    public Image stunEffect;

    //  ���� ������ ������ �ִ� ����.
    BossFSM bF;
    PlayerState pS;
    float temp;
    float temp2;

    //  �÷��̾� ���� Ÿ��. true=���Ÿ�,  false=�ٰŸ�
    public static bool playerWeaponType = true;


    // Start is called before the first frame update
    void Awake()
    {
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();
        pS = GameObject.Find("Player").GetComponent<PlayerState>();

        //  ���� ü�¿� ����� ����ü�¹� ����
        temp = BossHealthBar.rect.width / bF.maxBossHP;
        temp2 = playerHealthBar.rect.width / pS.playerMaxHP;
        // ���� �޴��� ���� �ð� ��
        //int hour = (int)(playTime / 3600);
        //int min = (int)((playTime - hour * 3600) / 60);
        //int second = (int)(playTime % 60);

        //playTimeTxt.text = string.Format("{0:n0}", hour) + ":" + string.Format("{0:n0}", min) + ":" + string.Format("{0:n0}", second);
    }

    public void GameStart()
    {


        isBattle = true;

        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }

   

    // Update is called once per frame
    void Update()
    {
        //  ���� ���� ü���� ���� ü�� UI�� ����.
        BossHealthBar.sizeDelta = new Vector2(temp * bF.HP , 20);

        //  ���� �÷��̾� ü���� �÷��̾� ü�� UI�� ����.
        playerHealthBar.sizeDelta = new Vector2(temp2 * pS.GetPlayerHP(), 25);
        playerHealthTxt.text = pS.GetPlayerHP()+" / 100";

        if (isBattle == true)
        {

           // playTime2 == playTime; 
            playTime += Time.deltaTime;
        }
        if (Input.GetButton("Zoom") && PlayerState.stunCheck ==false)
        {
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            crosshair.gameObject.SetActive(false);
        }
    }


    private void LateUpdate()
    {
        BossStageTxt.text = bossname;
        // ���� �ð� ǥ��
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int second = (int)(playTime % 60);

        playTimeTxt.text = string.Format("{0:00}", hour) + " : " + string.Format("{0:00}", min) + " : " + string.Format("{0:00}", second);



        if (Input.GetKey(KeyCode.Escape))
        {
            isBattle = false;
            playTime2Txt.text = playTimeTxt.text;


            menuCam.SetActive(true);
            gameCam.SetActive(false);

            menuPanel.SetActive(true);
            gamePanel.SetActive(false);

            player.gameObject.SetActive(false);


        }

        //playerHealthTxt.text = player.health + " / " + player.maxHealth;
        //playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        //if (player.equipWeapon == null)
        //{

        //    playerAmmoTxt.text = " - / " + player.ammo;\

        //}
        //else if (player.equipWeapon.type == weapon1Img.type.Melee)
        //{

        //    playerAmmoTxt.text = "- / " + player.ammo;

        //}
        //else 
        //{ playerAmmoTxt.text = player.equipWeapon.curAmmo + " / " + player.ammo; }

        // ���� ���� ü��
        // BossHealthBar.localScale = new Vector3(BossHealthBar.curHealth / Boss.maxHealth, 1, 1) ;
    }

}
