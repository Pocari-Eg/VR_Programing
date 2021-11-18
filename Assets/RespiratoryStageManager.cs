using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RespiratoryStageManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] RespiratoryStage;
    [SerializeField]
    GameObject StartPosition;

    [SerializeField]
    GameObject StartDialog;

    [SerializeField]
    GameObject Player;

  

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StageOn()
    {
        
        Player.GetComponent<CapsuleCollider>().enabled = true;
        Player.GetComponent<CharacterController>().center = new Vector3(0,-1.0f,0);
        Player.GetComponent<CharacterController>().radius = 0.5f;
        Player.GetComponent<CharacterController>().height = 3;



        for (int i = 0; i < RespiratoryStage.Length; i++)
        {
            RespiratoryStage[i].SetActive(true);
        }
        

    }

    public void StageStart()
    {
        StartDialog.SetActive(false);
        Player.GetComponent<ForceMove>().viewMode = 1;
   
      
    }
    public void StageOff()
    {
        for (int i = 0; i < RespiratoryStage.Length; i++)
        {
            RespiratoryStage[i].SetActive(false);
        }
        Player.GetComponent<CapsuleCollider>().enabled = false;
    }

    public   Vector3 GetStartPosition()
    {
        return StartPosition.transform.position;
    }
}
