using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public enum StageNum {Bonus, Respiratory, Stomach, Intestine }

    public static GameManager instasnce;
 
    BonusStageManager m_bonusStage;
    RespiratoryStageManager m_PipeStage;
    // Start is called before the first frame update

    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject m_Camera;

    [SerializeField]
    Text CellCount;
    int cellnum;


    public StageNum curStage = StageNum.Respiratory;


    private void Awake()
    {
        instasnce = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        m_bonusStage = this.gameObject.GetComponent<BonusStageManager>();
        m_PipeStage= this.gameObject.GetComponent<RespiratoryStageManager>();

    }
    void Start()
    {
        StartSet();
        m_PipeStage.StageOn();
        UIControllOn();
    }

    // Update is called once per frame
    void Update()
    {
        
      
        cellnum = Player.GetComponent<PlayerData>().GetCellCount();
        CellCount.text = cellnum.ToString();
    }

    void StartSet()
    {
        Player.GetComponent<PlayerData>().SetCellCount(15);
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
}
