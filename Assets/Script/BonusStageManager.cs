using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusStageManager : MonoBehaviour
{
    public static BonusStageManager instance;

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

    
    float SpawnTime = 3.5f;

    public bool GameStart;

    int cellnum;
    float[] random = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;


    }

    // Update is called once per frame
    void Update()
    {
        if (GameStart)
        {
            SpawnCell();


            if (cellnum <= 0)
            {
               
                GameStart = false;


            }
        }
        else
        {
            GameObject cell = GameObject.FindGameObjectWithTag("Cell");
            if (cell != null)
            {
                Object.Destroy(cell);
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

            Instantiate(WBCPrefeb, new Vector3(Player.transform.position.x + ranX, Player.transform.position.y + ranY, Player.transform.position.z + ranZ), Quaternion.identity);

        }
        else
        {
            Instantiate(CellPrefeb, new Vector3(Player.transform.position.x + ranX, Player.transform.position.y + ranY, Player.transform.position.z + ranZ), Quaternion.identity);
        }

    }

    public void OnGameStart()
    {
        StageOn();
        GameStart = true;
        StartUI.SetActive(false);
        GameManager.instasnce.UIControllOff();

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
                break;
            case GameManager.StageNum.Stomach:
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
     
        Player.GetComponent<SphereCollider>().enabled = false;
    }
  
    public void startUiOn()
    {
        GameManager.instasnce.UIControllOn();
        StartUI.SetActive(true);
       
    }
}


 


