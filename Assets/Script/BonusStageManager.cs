using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusStageManager : MonoBehaviour
{
    public static BonusStageManager instance;


    public List<GameObject> CellList;
    [SerializeField]
    GameObject BonusStage;
    [SerializeField]
    GameObject CellPrefeb;
    [SerializeField]
    GameObject WBCPrefeb;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject StartUI;

    [SerializeField]
    GameObject m_Camera;
    [SerializeField]
    GameObject TimeBar;
    public  Text text_Timer;
    private float time_current;
    [SerializeField]
    private float time_Max = 60f;
    private bool isEnded;

    float SpawnTime = 3.5f;

    public bool GameStart;

    int cellnum;



 public   GameObject StageChangeUI;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

       
    }

    // Update is called once per frame
    void Update()
    {
        TimeBar.GetComponent<Image>().fillAmount = time_current / 60f;
        if (GameStart)
        {
            
            SpawnCell();
            Check_Timer();

            if (cellnum <= 0)
            {
                End_Timer();
            }
        }
    
    }
    private void FixedUpdate()
    {
       
        cellnum = Player.GetComponent<PlayerData>().GetCellCount();
    }

    void SpawnCell()
    {
        if (SpawnTime > 0.0f)
        {
            SpawnTime -= Time.deltaTime;
        }
        else
        {
            RandomSpwan();
            SpawnTime = 3.5f;
        }
            
      }

    void RandomSpwan()
    {

        int x = Random.Range(1, 10);
        float ranX = Random.Range(-9.0f, 9.0f);
        float ranY = Random.Range(3.0f, 6.0f);
        float ranZ = Random.Range(-9.0f, 9.0f);
        if (x < 3)
        {

            CellList.Add(  Instantiate(WBCPrefeb, new Vector3(Player.transform.position.x + ranX, Player.transform.position.y + ranY, Player.transform.position.z + ranZ), Quaternion.identity));

        }
        else
        {
            CellList.Add( Instantiate(CellPrefeb, new Vector3(Player.transform.position.x + ranX, Player.transform.position.y + ranY, Player.transform.position.z + ranZ), Quaternion.identity));
        }

    }

    public void OnGameStart()
    {
        StageOn();
        GameStart = true;
        StartUI.SetActive(false);
        GameManager.instasnce.UIControllOff();
        Reset_Timer();
    }


    public void StageOn()
    {
        Player.GetComponent<CapsuleCollider>().enabled = false;



        Player.GetComponent<CharacterController>().center = new Vector3(0, 0.0f, 0);
        Player.GetComponent<CharacterController>().radius = 1.5f;
        Player.GetComponent<CharacterController>().height = 1;
        switch (GameManager.instasnce.curStage)
        {
            case GameManager.StageNum.Respiratory:
                this.gameObject.GetComponent<RespiratoryStageManager>().StageOff();
                GameManager.instasnce.curStage = GameManager.StageNum.Stomach;
                break;
            case GameManager.StageNum.Stomach:
                this.gameObject.GetComponent<StomachStageManager>().StageOff();
                GameManager.instasnce.curStage = GameManager.StageNum.Intestine;
          
                break;
            case GameManager.StageNum.Intestine:
               
                break;
            default:
                break;
        }

           BonusStage.SetActive(true);
        BonusStage.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - 2.18f, Player.transform.position.z);
      
    }
    public void StageOff()
    {
            BonusStage.SetActive(false);
        
    }
  
    public void startUiOn()
    {
        GameManager.instasnce.UIControllOn();
        StartUI.SetActive(true);
       
    }

    private void Check_Timer()
    {

        if (0 < time_current)
        {
            time_current -= Time.deltaTime;
            text_Timer.text = $"{time_current:N1}";
            Debug.Log(time_current);
        }
        else if (!isEnded)
        {
            End_Timer();
        }


    }

    private void End_Timer()
    {
        Debug.Log("End");
        time_current = 0;
        text_Timer.text = $"{time_current:N1}";
        isEnded = true;
        StageEnd();
    }


    public void Reset_Timer()
    {
      
        time_current = time_Max;
        text_Timer.text = $"{time_current:N1}";
        isEnded = false;
        Debug.Log("Start");
    }

    void StageEnd()
    {
        StageClear();
        Debug.Log("½ÇÇàµÊ");
        if (GameManager.instasnce.curStage == GameManager.StageNum.Intestine)
        {
            GameManager.instasnce.GameEndUIon();
        }
        else
        {

            StageChangeUI.SetActive(true);
        }
      
        GameManager.instasnce.UIControllOn();

    }
    public  void StageChange()
    {
        StageChangeUI.SetActive(false);
        StageOff();
        GameManager.instasnce.StageChange();
    }

    public void StageClear()
    {
     
        GameStart = false;
        for (int i = 0; i < CellList.Count; i++)
        {
            Object.Destroy(CellList[i]);
        }
    }
}


 


