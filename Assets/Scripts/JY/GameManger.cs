using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{

    PlayerState ps;
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
    public GameObject DeadPanel;


    public GameObject Scoreball1;
    public GameObject Scoreball2;
    public GameObject Scoreball3;
   

    public Text maxTimeTxt;
    public Text BossStageTxt;
    public Text playTimeTxt;
    public GameObject VictoryButton;

    public Text playerHealthTxt;
    public RectTransform playerHealthBar;
    public Text playerStaminaTxt;
    public RectTransform playerStaminaBar;
    public Text playerAmmoTxt;
    public Text playerCoinTxt;

    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image weapon4Img;
    public Image weaponRImg;
    

    // 보스 흔적 UI
    public GameObject Trace;
    public GameObject Trace1;
    public GameObject Trace2;
    public GameObject Trace3;
    public GameObject Trace4;

    // Equip 인벤토리
    public GameObject gun;
    public GameObject sword;
    public GameObject DevStone;

    public RectTransform BossHealthGroup;
    public RectTransform BossHealthBar;

    public Image crosshair;
    public Image stunEffect;

    //  보스 정보를 가지고 있는 변수.
    BossFSM bF;
    PlayerState pS;

    float temp;
    float temp2;
    float temp3;

    public PlayerMove pm;
    //  플레이어 공격 타입. true=원거리근거리,  false=
    public bool Melee = false;


    // Start is called before the first frame update
    void Awake()
    {
        MeleeCheck();
        bF = GameObject.Find("Boss").GetComponent<BossFSM>();
        pS = GameObject.Find("Player").GetComponent<PlayerState>();

        

        //  보스 체력에 비례한 보스체력바 비율
        temp = BossHealthBar.rect.width / bF.maxBossHP;
        temp2 = playerHealthBar.rect.width / pS.playerMaxHP;
        temp3 = playerStaminaBar.rect.width / pS.playerMaxMP;
        // 메인 메뉴의 현재 시간 초
        //int hour = (int)(playTime / 3600);
        //int min = (int)((playTime - hour * 3600) / 60);
        //int second = (int)(playTime % 60);

        //playTimeTxt.text = string.Format("{0:n0}", hour) + ":" + string.Format("{0:n0}", min) + ":" + string.Format("{0:n0}", second);
    }
    

public void GameStart()
    {
        Cursor.visible = false;
        isBattle = true;
        VictoryButton.SetActive(false);

        DeadPanel.SetActive(false);
        menuCam.SetActive(false);
        gameCam.SetActive(true);
        pm.gamecamOn = true;
        pm.check = false;

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }

   

    // Update is called once per frame
    void Update()
    {
        //  현재 보스 체력을 보스 체력 UI에 설정.
        BossHealthBar.sizeDelta = new Vector2(temp * bF.HP , 20);

        //  현재 플레이어 체력을 플레이어 체력 UI에 설정.
        playerHealthBar.sizeDelta = new Vector2(temp2 * pS.GetPlayerHP(), 25);
        playerHealthTxt.text = pS.GetPlayerHP()+" / "+pS.playerMaxHP;

        //  현재 플레이어 스태미나를 플레이어 스태미나 UI에 설정.
        playerStaminaBar.sizeDelta = new Vector2(temp3 * pS.GetPlayerMP(), 25);
        playerStaminaTxt.text = (int)pS.GetPlayerMP() + " / " + pS.playerMaxMP;

        if (isBattle == true)
        {

           // playTime2 == playTime; 
            playTime += Time.deltaTime;
        }
        if (PlayerState.playerZoomCheck == true && PlayerState.stunCheck ==false)
        {
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            crosshair.gameObject.SetActive(false);
        }

        

    }
    void MeleeCheck()
    {
        Melee = false;
    }

    

    public void LateUpdate()
    {
        BossStageTxt.text = bossname;
        // 현재 시간 표기
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int second = (int)(playTime % 60);

        playTimeTxt.text = string.Format("{0:00}", hour) + " : " + string.Format("{0:00}", min) + " : " + string.Format("{0:00}", second);


        void ScoreCheck()
        {
           

            if (bF.isDead != false)
            {
                              

                if (min <= 0)
                {
                   
                    Scoreball1.SetActive(true);
                    Scoreball2.SetActive(true);
                    Scoreball3.SetActive(true);
                }
                else if (second <= 6)
                {
                    print("죽");
                    Scoreball1.SetActive(false);
                    Scoreball2.SetActive(true);
                    Scoreball3.SetActive(true);
                }
                else if (min <= 10)
                {
                    Scoreball1.SetActive(false);
                    Scoreball2.SetActive(false);
                    Scoreball3.SetActive(true);
                }
                else if (min <= 13)
                {
                    Scoreball1.SetActive(false);
                    Scoreball2.SetActive(false);
                    Scoreball3.SetActive(false);
                }


            }
            else
            { }    
        }

        


        

        if (Input.GetKey(KeyCode.Escape))
        {

            bF.VictoryPanel.SetActive(false);
            bF.VictoryButton.SetActive(false);

            Cursor.visible = true;

            isBattle = false;

            Vector3 anchor = new Vector3(0, 14, -10);
            Quaternion anchor2 = new Quaternion(45, 0, 0, 0);

            menuCam.transform.position = player.transform.position + anchor;
            menuCam.transform.rotation = anchor2;


            menuCam.SetActive(true);
            gameCam.SetActive(false);
            pm.gamecamOn = false;
            pm.check = true;

            menuPanel.SetActive(true);
            gamePanel.SetActive(false);

           // player.gameObject.SetActive(false);


        }


        if (pS.GetPlayerHP() == 0)
        {
            
            DeadPanel.SetActive(true);
            VictoryButton.SetActive(true);
            gamePanel.SetActive(false);
            menuPanel.SetActive(false);
           
            ps.isDead = true;
            ScoreCheck(); 
            Scoreball1.SetActive(false);
            Scoreball2.SetActive(false);
            Scoreball3.SetActive(false);

            Destroy(gameObject);
        }

        if (bF.isDead == true)
        {
          
            ScoreCheck();
        }


            // ** 간소화 예정
            #region (JY) Trace UI 간소화 예정
            if (pm.hasTrace [0] == true) 
        {
            Trace.SetActive(true);
        }
        if (pm.hasTrace[1] == true)
        {
            Trace1.SetActive(true);
        }
        if (pm.hasTrace[2] == true)
        {
            Trace2.SetActive(true);
        }
        if (pm.hasTrace[3] == true)
        {
            Trace3.SetActive(true);
        }
        if (pm.hasTrace[4] == true)
        {
            Trace4.SetActive(true);
        }

        if (pm.hasItem[0] == true)
        {
            DevStone.SetActive(true);
        }
        #endregion

        
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
