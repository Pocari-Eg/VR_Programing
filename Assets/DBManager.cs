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
        m_Reference = FirebaseDatabase.DefaultInstance.GetReference("user");  //�����ͺ��̽� �ν��Ͻ� ���� 
    }
    void ReadUserData()
    {
       m_Reference.GetValueAsync().ContinueWithOnMainThread(task =>  // �񵿱��  �����͸� �о�´�.
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
           for (int i = 0; i < snapshot.ChildrenCount; i++)                                                        //�����͸� �����ϱ� ���� Dictionary�� ������ ���� 
           {
                   string name = snapshot.Child(i.ToString()).Child("Username").Value.ToString();
                   int score = int.Parse(snapshot.Child(i.ToString()).Child("Score").Value.ToString());
                   Rank.Add(name, score);
               }
           }
       });
        StartCoroutine(LoadData());          //�ٷ� �����͸� ����ϸ� ������ �ȵ� 3�ʰ��� �ε� �ð� �ο�
    }

    public   void WriteUserData( )                            //������ ������ DB�� �Է� 
    {
        string username = this.gameObject.GetComponent<PlayerInfo>().playerName.GetComponent<Text>().text;             // �÷��̾� �̸�
        int Score = int.Parse(GameManager.instasnce.CellCount.text);       //�÷�������
          
        m_Reference.Child(LastId.ToString()).Child("Username").SetValueAsync(username);          //  User��  Key�� �߰��ϰ� ���������� �Է�
        m_Reference.Child(LastId.ToString()).Child("Score").SetValueAsync(Score);

        TextBox.SetActive(false);
        NextButton.GetComponent<Button>().interactable = false;
        NextButton.GetComponent<BoxCollider>().enabled = false;
        ReadUserData();                                                                                                                        // ���� �Է��� ���ο� ������ ����.
    }

    public void WriteUserData(string Userid,string username, int Score)
    {
  

        m_Reference.Child(Userid).Child("Username").SetValueAsync(username);
        m_Reference.Child(Userid).Child("Score").SetValueAsync(Score);

    }



   public void DataSort() // DB���� �ҷ��� �����͸� ������ ���������� ����
    {
    

        var SortList = from pair in Rank
                    orderby pair.Value descending
                    select pair;


        int i = 0;
        foreach (KeyValuePair<string, int> pair in SortList)
        {
            if (i > 4)
                break;

            Debug.Log("���� ���");
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




