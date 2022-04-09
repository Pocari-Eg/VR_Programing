using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
using System.Linq;

public class DBManager : MonoBehaviour
{

    [SerializeField]
    GameObject LeaderBoad;

    [SerializeField]
    GameObject TextBox;
    [SerializeField]
    GameObject NextButton;
    DatabaseReference m_Reference;

    public Text[] Score;
    public Text[] Username;

    [SerializeField]
    GameObject ScoreBoard;
    [SerializeField]
    GameObject LoadingText;
    
    [SerializeField]
    Dictionary<string, int> Rank = new Dictionary<string, int>();


    int LastId;
  
    void Start()
    {
        m_Reference = FirebaseDatabase.DefaultInstance.GetReference("user");  //데이터베이스 인스턴스 생성 
    }
    void ReadUserData()
    {
       m_Reference.GetValueAsync().ContinueWithOnMainThread(task =>  // 비동기로  데이터를 읽어온다.
       {
       if (task.IsFaulted)
       {
               Debug.Log("Connect Fail");
       }
       else if (task.IsCompleted)
       {
               Debug.Log("Connect  Success");
               DataSnapshot snapshot = task.Result;
               LastId =(int) snapshot.ChildrenCount;                         
               Rank.Clear();
           for (int i = 0; i < snapshot.ChildrenCount; i++)                                                        //데이터를 정렬하기 위해 Dictionary에 데이터 저장 
           {
                   string name = snapshot.Child(i.ToString()).Child("Username").Value.ToString();
                   int score = int.Parse(snapshot.Child(i.ToString()).Child("Score").Value.ToString());
                   Rank.Add(name, score);
               }
           }
       });
        StartCoroutine(LoadData());          //바로 데이터를 출력하면 갱신이 안되 3초간의 로딩 시간 부여
    }

    public   void WriteUserData( )                            //유저의 정보를 DB에 입력 
    {
        string username = this.gameObject.GetComponent<PlayerInfo>().playerName.GetComponent<Text>().text;             // 플레이어 이름
        int Score = int.Parse(GameManager.instasnce.CellCount.text);       //플레이점수
          
        m_Reference.Child(LastId.ToString()).Child("Username").SetValueAsync(username);          //  User에  Key를 추가하고 하위정보를 입력
        m_Reference.Child(LastId.ToString()).Child("Score").SetValueAsync(Score);

        TextBox.SetActive(false);
        NextButton.GetComponent<Button>().interactable = false;
        NextButton.GetComponent<BoxCollider>().enabled = false;
        ReadUserData();                                                                                                                        // 값을 입력후 새로운 정보를 갱신.
    }

    public void WriteUserData(string Userid,string username, int Score)
    {
  

        m_Reference.Child(Userid).Child("Username").SetValueAsync(username);
        m_Reference.Child(Userid).Child("Score").SetValueAsync(Score);

    }



   public void DataSort() // DB에서 불러온 데이터를 점수가 높은순으로 정렬
    {
    

        var SortList = from pair in Rank
                    orderby pair.Value descending
                    select pair;


        int i = 0;
        foreach (KeyValuePair<string, int> pair in SortList)
        {
            if (i > 4)
                break;

            Debug.Log("정렬 출력");
            Score[i].text = pair.Value.ToString();
            Username[i].text =pair.Key;
            i++;

        }
        ScoreBoard.SetActive(true);
    }
    IEnumerator LoadData()
    {
        ScoreBoard.SetActive(false);
        LoadingText.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        LoadingText.SetActive(false);
        DataSort();
        
    }
    public void Recycle()
    {
        StartCoroutine(LoadData());
    }

   public void LeaderBoardOpenClose()
    {
        if (LeaderBoad.active == true)
        {
            LeaderBoad.SetActive(false);
        }
        else if(LeaderBoad.active==false)
        {
            ReadUserData();
            LeaderBoad.SetActive(true);
        }
       
    }
    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot
    }
}




