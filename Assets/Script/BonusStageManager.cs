using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusStageManager : MonoBehaviour
{


    [SerializeField]
    GameObject[] BonusStage;
    [SerializeField]
    GameObject CellPrefeb;
    [SerializeField]
    GameObject WBCPrefeb;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject StartUI;

    float SpawnTime = 3.5f;

   

    int cellnum;
    float[] random = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


    bool GameStart = false;
    // Start is called before the first frame update
    void Start()
    {
       
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
        GameStart = true;
        StartUI.SetActive(false);
    }

   
    public void StageOn()
    {
       for(int i=0;i< BonusStage.Length; i++)
        {
            BonusStage[i].SetActive(true);
        }
        Player.GetComponent<SphereCollider>().enabled = true;
    }
    public void StageOff()
    {
        for (int i = 0; i < BonusStage.Length; i++)
        {
            BonusStage[i].SetActive(false);
        }
        Player.GetComponent<SphereCollider>().enabled = false;
    }
  
}


 


