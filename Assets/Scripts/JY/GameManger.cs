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

    //  보스 정보를 가지고 있는 변수.
    BossFSM bF;
    float temp;


    // Start is called before the first frame update
    void Awake()
    {
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();
        //  보스 체력에 비례한 보스체력바 비율
        temp = BossHealthBar.rect.width / bF.bossHp;
        // 메인 메뉴의 현재 시간 초
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
        //  현재 보스 체력을 보스 체력 UI에 설정.
        BossHealthBar.sizeDelta = new Vector2(temp * bF.bossHp , 20);

        if (isBattle == true)
        {

           // playTime2 == playTime; 
            playTime += Time.deltaTime;

        }
       


    }


    private void LateUpdate()
    {
        BossStageTxt.text = bossname;
        // 현재 시간 표기
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

        // 보스 몬스터 체력
        // BossHealthBar.localScale = new Vector3(BossHealthBar.curHealth / Boss.maxHealth, 1, 1) ;
    }

}
