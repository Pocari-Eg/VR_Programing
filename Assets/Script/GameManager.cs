using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    BonusStageManager m_bonusStage;
    PipeStageManager m_PipeStage;
    // Start is called before the first frame update

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject EndUI;
    [SerializeField]
    Text CellCount;
    int cellnum;

    public bool BounsStageOn = false;

    public bool PipeStageOn = false;
 
    private void Awake()
    {
        m_bonusStage = this.gameObject.GetComponent<BonusStageManager>();
        m_PipeStage= this.gameObject.GetComponent<PipeStageManager>();

    }
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (!BounsStageOn)
        {
          
            m_bonusStage.enabled = false;
            m_bonusStage.StageOff();
        }
      else  if (BounsStageOn)
        {
            Player.GetComponent<ForceMove>().SetViewMode(2);
            m_bonusStage.enabled = true;
            m_bonusStage.StageOn();
        }
 
         if (!PipeStageOn)
        {
          
            m_PipeStage.enabled = false;
            m_PipeStage.StageOff();
        }

      else  if (PipeStageOn)
        {
            Player.GetComponent<ForceMove>().SetViewMode(1);
            m_PipeStage.enabled = true;
            m_PipeStage.StageOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player.GetComponent<PlayerData>().GetCellCount() <= 0)
        {
           
            EndUI.SetActive(true);
            Player.GetComponent<ForceMove>().viewMode = 2;
        }
        cellnum = Player.GetComponent<PlayerData>().GetCellCount();
        CellCount.text = cellnum.ToString();
    }


    public void GameQuit()
    {
        Application.Quit();
    }
}
