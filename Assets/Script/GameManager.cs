using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public enum StageNum {Bonus, Respiratory, Stomach, Intestine }

    public static GameManager instasnce;
 
    BonusStageManager m_bonusStage;
    RespiratoryStageManager m_ResStage;
    StomachStageManager m_StomStage;
    // Start is called before the first frame update

    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject m_Camera;

    [SerializeField]
    Text CellCount;
    int cellnum;

    [SerializeField]
    GameObject GameOverUI;

    public StageNum curStage = StageNum.Respiratory;


    private void Awake()
    {
        instasnce = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        m_bonusStage = this.gameObject.GetComponent<BonusStageManager>();
        m_ResStage= this.gameObject.GetComponent<RespiratoryStageManager>();
        m_StomStage = this.gameObject.GetComponent<StomachStageManager>();
    }
    void Start()
    {

        
        StartSet();
        UIControllOn();
        StageChange();
       

    }

    // Update is called once per frame
    void Update()
    {
        
      
        cellnum = Player.GetComponent<PlayerData>().GetCellCount();
        CellCount.text = cellnum.ToString();

        if (cellnum <= 0)
        {
            Gameover();
        }
    }

    void StartSet()
    {
       
        Player.GetComponent<ForceMove>().viewMode = 1 ;
    }
    public void GameQuit()
    {
        Application.Quit();
    }

    public void UIControllOn()
    {
        m_Camera.GetComponent<CameraPointer>().enabled = false;
        m_Camera.GetComponent<CameraPointerExt>().enabled = true;
    }

    public void UIControllOff()
    {
        m_Camera.GetComponent<CameraPointer>().enabled = true;
        m_Camera.GetComponent<CameraPointerExt>().enabled = false;
    }


    public void StageChange()
    {
        switch (GameManager.instasnce.curStage)
        {
            case GameManager.StageNum.Respiratory:
                m_ResStage.StageOn();
                break;
            case GameManager.StageNum.Stomach:
                m_StomStage.StageOn();
                break;
            case GameManager.StageNum.Intestine:
                break;
            default:
                break;
        }
    }
    void Gameover()
    {
        m_bonusStage.StageClear();
        m_bonusStage.StageOff();
        m_bonusStage.StageChangeUI.SetActive(false);
        m_ResStage.StageOff();
        m_StomStage.StageOff();

        GameOverUI.SetActive(true);
        UIControllOn();
    }


}
